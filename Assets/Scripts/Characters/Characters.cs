using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;
using Unity.AI.Navigation;
using TMPro;
using System.IO;

public class Characters : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] protected float _maxHighDistance;
    [SerializeField] protected float _minHighDistance;
    [SerializeField] protected float _minHorizontalJump;
    [SerializeField] protected float _maxHorizontalJump;
    [SerializeField] protected Transform _playerPivot;
    [SerializeField] protected Animator _animator;


    private Action ReachDestiny;
    [SerializeField]protected Vector3 _currentDestiny;
    protected Vector3 _finalDestiny;
    protected Vector3 _startJump;
    protected Vector3 _endJump;
    protected Vector3 _jumpDestiny;
    protected Vector3 _lookAt;
    protected bool _isMoving;
    protected Interactable _InteractableYetToInteract;

    private float _timer;
    private bool _jump;
    private bool _readyToJump;
    private Vector3[] _cornors;


    public Interactable Interactable { get; set; }
    [field: SerializeField] public bool HasKey { get; set; }

    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    protected void Awake()
    {
        //print("Stopping distance is" + _navMeshAgent.stoppingDistance + 0.1f);
        HasKey = false;
    }

    /// <summary>
    /// Called simply when the character moves
    /// </summary>
    /// <param name="destiny"></param>
    public virtual void Move(Vector3 destiny)
    {
        _finalDestiny = destiny;
        if (Mathf.Abs(destiny.y - _playerPivot.position.y) >= _minHighDistance && Mathf.Abs(destiny.y - _playerPivot.position.y) <= _maxHighDistance && Interactable == null)
        {
            _jump = true;
            _isMoving = true;

            CalculateJumpStar(destiny);
            _currentDestiny = _startJump;
            CalculateJumpEnd(destiny);
            _navMeshAgent.SetDestination(_currentDestiny);
        }
        else
        {
            _currentDestiny = destiny;
            Vector2 cPosition = new Vector2(transform.position.x, transform.position.z);
            Vector2 dPosition = new Vector2(_currentDestiny.x, _currentDestiny.z);
            _jump = false;
            
            NavMeshPath path = new NavMeshPath();
            _navMeshAgent.CalculatePath(destiny, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                if (Vector2.Distance(cPosition, dPosition) >= _minHorizontalJump && Vector2.Distance(cPosition, dPosition) <= _maxHorizontalJump)
                {
                    _jump = true;
                    _isMoving = true;
                    CalculateJumpStar(destiny);
                    _currentDestiny = _startJump;
                    CalculateJumpEnd(destiny);
                    _navMeshAgent.SetDestination(_currentDestiny);
                }
            }
            else
            {
                _navMeshAgent.SetDestination(destiny);
            }       
        }
    }

 


    private void CalculateJumpStar(Vector3 destiny)
    {
        NavMeshPath path = new NavMeshPath();
        _navMeshAgent.CalculatePath(destiny, path);
        _cornors = path.corners;
        _startJump = _cornors[_cornors.Length - 1] + (transform.position - _playerPivot.position);


    }
    private void CalculateJumpEnd(Vector3 destiny)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(destiny, transform.position, _navMeshAgent.areaMask, path);
        _cornors = path.corners;
        _endJump = _cornors[_cornors.Length - 1] + (transform.position - _playerPivot.position);
    }

    
    public virtual void Move(Vector3 destiny, Interactable interactable, Vector3 lookAt)
    {
        _currentDestiny = destiny;
        _navMeshAgent.SetDestination(_currentDestiny);
        _isMoving = true;
        _InteractableYetToInteract = interactable;

        _lookAt = lookAt;

    }
    private void Jump()
    {
        _timer += Time.deltaTime;
        Vector3 destiny = Vector3.Lerp(_startJump, _endJump, _timer);
        _navMeshAgent.Warp(destiny);
        if (_timer > 1)
        {
            _timer = 0;
            _jump = false;
            _readyToJump = false;
            _navMeshAgent.updatePosition = true;
            _navMeshAgent.SetDestination(_finalDestiny);
        }

    }





    /// <summary>
    /// Plays the animation of dying
    /// </summary>
    public void Die()
    {
        _animator.SetBool("IsDead", true);
    }
    /// <summary>
    /// Called by the animation event when the animation ends
    /// Reloads current scene
    /// </summary>
    public void LoadWhenDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// Called when a character moves and needs
    /// to look at one direction
    /// </summary>
    /// <param name="destiny">Where the character goes</param>
    /// <param name="lookAt">Position where the character will rotate</param>


    protected void Interact(Interactable interactable)
    {
        if (Interactable == null)
        {

            interactable.Interact(this);
            _InteractableYetToInteract = null;
        }
        else
        {
            Interactable.StopInteract();
        }

    }

    protected void FixedUpdate()
    {
        Vector2 cPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 dPosition = new Vector2(_currentDestiny.x, _currentDestiny.z);
        if (Vector2.Distance(cPosition, dPosition) <= _navMeshAgent.stoppingDistance + 0.1f && _isMoving)
        {
            if (_jump)
            {
                _readyToJump = true;

            }


            //Makes the rotation of the character
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.transform.LookAt(_lookAt);
            _navMeshAgent.updateRotation = true;

            if (_InteractableYetToInteract != null)
                Interact(_InteractableYetToInteract);
            _isMoving = false;
        }
        if (_readyToJump)
        {

            Jump();

        }

    }


}
