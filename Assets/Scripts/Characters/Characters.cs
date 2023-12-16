using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;
using Unity.AI.Navigation;

public class Characters : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] protected float        _maxHighDistance;
    [SerializeField] protected float        _minHighDistance;
    [SerializeField] protected Transform    _playerPivot;
    [SerializeField] protected Animator     _animator;
    [SerializeField] private    Vector3[]    _cornors;
    [SerializeField] private bool _canMOve;
    

    private Action ReachDestiny;
    protected Vector3       _destiny;
    protected Vector3       _startJump;
    protected Vector3       _endJump;
    protected Vector3       _jumpDestiny;
    protected Vector3       _lookAt;
    protected bool          _isMoving;
    protected Interactable  _InteractableYetToInteract;

    private float _timer;
    private bool _jump;
    private bool _readyToJump;

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
    public virtual void Move( Vector3 destiny)
    {
        
        if ((int)Mathf.Abs(destiny.y - _playerPivot.position.y)>= _minHighDistance  && (int)Mathf.Abs(destiny.y - _playerPivot.position.y) <=_maxHighDistance && Interactable == null)
        {  
            _jump = true;
            _isMoving = true;
            
            CalculateJumpStar(destiny);
            _destiny = _startJump;
            CalculateJumpEnd(destiny);
            _navMeshAgent.SetDestination(_destiny);
        }
        else
        {
            _jump = false;
            _navMeshAgent.SetDestination(destiny);
        }
        

    }

    private void CalculateJumpStar(Vector3 destiny)
    {
        NavMeshPath path = new NavMeshPath();
        _navMeshAgent.CalculatePath(destiny, path);
        _cornors = path.corners;
        _startJump = _cornors[_cornors.Length - 1] + (transform.position-_playerPivot.position);

        
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
        _destiny = destiny;
        _navMeshAgent.SetDestination(_destiny);
        _isMoving = true;
        _InteractableYetToInteract = interactable;
        _lookAt = lookAt;

    }
    private void Jump()
    {
        _navMeshAgent.updatePosition = false;
        _timer += Time.deltaTime;
        Vector3 destiny = Vector3.Lerp(_startJump,_endJump, _timer);
        _navMeshAgent.Warp(destiny);
        if (_timer >1)
        {
            _timer = 0;
            _jump = false;
            _readyToJump = false;
            _navMeshAgent.updatePosition = true;
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
        //print((transform.position - _destiny).magnitude);
        if ((transform.position - _destiny).magnitude <= _navMeshAgent.stoppingDistance + 0.1f && _isMoving)
        {
            if(_jump)
            {
                _readyToJump = true;
               
            }
                

            //Makes the rotation of the character
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.transform.LookAt(_lookAt);
            _navMeshAgent.updateRotation = true;
            if(_InteractableYetToInteract != null)
                Interact(_InteractableYetToInteract);
            _isMoving = false;
        }
        if (_readyToJump)
        {
            
            Jump();
           
        }

    }

   
}
