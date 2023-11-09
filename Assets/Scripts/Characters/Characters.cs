

// Ignore Spelling: Nav

using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.CullingGroup;

public class Characters : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;

    public Action ReachedDestiny;

    protected Vector3 _lookAt;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    /// <summary>
    /// Called simply when the character moves
    /// </summary>
    /// <param name="destiny"></param>
    public virtual void Move(Vector3 destiny)
    {
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
        _navMeshAgent.SetDestination(destiny);
        _lookAt = lookAt;
        
    }


    //protected virtual void CharacterChanged()
    //{
    //    // Safely raise the event for all subscribers
    //    ReachedDestiny?.Invoke(this);
    //}
}
