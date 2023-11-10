// Ignore Spelling: interactable Nav

using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the player character
/// </summary>
public class PlayerCharacter : Characters
{

    private void FixedUpdate()
    {
        Debug.Log(_lookAt);
        if (_navMeshAgent.remainingDistance <= 0 + _navMeshAgent.stoppingDistance && _lookAt != Vector3.zero)
        {
            _navMeshAgent.transform.LookAt(new Vector3(_lookAt.x, _lookAt.y, _lookAt.z));
            _lookAt = Vector3.zero;
            ReachedDestiny?.Invoke();
        }
    }


}
