using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the Mason character
/// </summary>
public class MasonCharacter : Characters 
{
    
    [SerializeField] private PlayerCharacter _playerCharacter;


    public override void Move(Transform transform, Vector3 destiny)
    {

            if ((int)Mathf.Abs(destiny.y - _playerPivot.position.y) >= _minHighDistance && (int)Mathf.Abs(destiny.y - _playerPivot.position.y) <= _maxHighDistance)
            {
                if (!transform.GetComponentInParent<BigBox>())
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
