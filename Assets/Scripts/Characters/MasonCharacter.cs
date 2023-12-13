using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the Mason character
/// </summary>
public class MasonCharacter : Characters 
{
    [SerializeField] private float _maxSoloHigh;
    [SerializeField] private PlayerCharacter _playerCharacter;


    public override void Move( Vector3 destiny)
    {

            if ((int)Mathf.Abs(destiny.y - _playerPivot.position.y) >= _minHighDistance && (int)Mathf.Abs(destiny.y - _playerPivot.position.y) <= _maxHighDistance)
            {
                if ((int)Mathf.Abs(destiny.y - _playerPivot.position.y)<= _maxSoloHigh)
                    _navMeshAgent.Warp(destiny);
                else
                {
                    if(_playerCharacter.NavMeshAgent.CalculatePath(destiny, new NavMeshPath()))
                    {
                        _playerCharacter.NavMeshAgent.SetDestination(destiny);
                    }

                }
                    
            }
            else
                _navMeshAgent.SetDestination(destiny);
        
    }

}
