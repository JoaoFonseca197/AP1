

// Ignore Spelling: Nav

using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.CullingGroup;

public class Characters : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] protected float _maxHighDistance;
    [SerializeField] protected float _minHighDistance;
    [SerializeField] protected Transform _playerPivot;

    public Action ReachedDestiny;

    protected Vector3   _destiny;
    protected Vector3   _lookAt;
    protected bool      _isMoving;

    //public bool IsMoving
    //{   
    //    get => _isInteracting;
    //    set { _isInteracting = value; } 
    //}
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    /// <summary>
    /// Called simply when the character moves
    /// </summary>
    /// <param name="destiny"></param>
    public virtual void Move(Vector3 destiny)
    {
        print((int)Mathf.Abs(destiny.y - _playerPivot.position.y));
        print((int)Mathf.Abs(destiny.y - _playerPivot.position.y));
        if ((int)Mathf.Abs(destiny.y - _playerPivot.position.y)>= _minHighDistance  && (int)Mathf.Abs(destiny.y - _playerPivot.position.y) <=_maxHighDistance )
        {
            _navMeshAgent.Warp(destiny);
        }
        else
            _navMeshAgent.SetDestination(destiny);
        
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
