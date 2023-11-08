// Ignore Spelling: interactable Nav

using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the player character
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Vector3         _lookAt;
    private Vector3 _test;
    public Action ReachedDestiny;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;


    /// <summary>
    /// Method used to make the NavMeshAgent of the player character move
    /// </summary>
    /// <param name="destiny">Place where the character will go</param>
    public void Move(Vector3 destiny)
    {
        _test = Vector3.zero;
        _navMeshAgent.SetDestination(destiny);

    }

    /// <summary>
    /// Method used to make the NavMeshAgent of the player character move
    /// and look to the object
    /// </summary>
    /// <param name="destiny">Place where the character will go</param>
    /// <param name="lookAt">Object to look at. Vector.zero ti no look if not anything</param>
    public void Move(Vector3 destiny, Vector3 lookAt)
    {

        _navMeshAgent.SetDestination(destiny);
        _lookAt = lookAt;
    }
    private void FixedUpdate()
    {
        if (_navMeshAgent.remainingDistance <= 0 + _navMeshAgent.stoppingDistance && _lookAt != Vector3.zero)
        {
            _navMeshAgent.transform.LookAt(new Vector3(_lookAt.x,_lookAt.y,_lookAt.z));
            _lookAt = Vector3.zero;
            ReachedDestiny();
        }
    }
}
