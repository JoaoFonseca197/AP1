using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Characters : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] protected float _maxHighDistance;
    [SerializeField] protected float _minHighDistance;
    [SerializeField] protected Transform _playerPivot;
    [SerializeField] protected Animator _animator;

    public Action ReachedDestiny;

    
    protected Vector3   _destiny;
    protected Vector3   _lookAt;
    protected bool      _isMoving;

    
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    /// <summary>
    /// Called simply when the character moves
    /// </summary>
    /// <param name="destiny"></param>
    public virtual void Move(Vector3 destiny)
    {
        if ((int)Mathf.Abs(destiny.y - _playerPivot.position.y)>= _minHighDistance  && (int)Mathf.Abs(destiny.y - _playerPivot.position.y) <=_maxHighDistance )
        {
            _navMeshAgent.Warp(destiny);
        }
        else
            _navMeshAgent.SetDestination(destiny);
        
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
    public virtual void Move(Vector3 destiny, Vector3 lookAt)
    {
        _destiny = destiny ;
        _navMeshAgent.SetDestination(destiny);
        _isMoving = true;
        
    }

}
