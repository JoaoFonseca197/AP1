using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the Mason character
/// </summary>
public class MasonCharacter : Characters 
{
    
    [SerializeField] private PlayerCharacter _playerCharacter;

    //Property that tells if this character is following or not
    public bool IsFollowing {get;set;}

    private void FixedUpdate()
    {
        
        //If true follows the player character
        if (IsFollowing)
            _navMeshAgent.SetDestination(_playerCharacter.gameObject.transform.position);

        if (_navMeshAgent.remainingDistance <= 0 + _navMeshAgent.stoppingDistance && _lookAt != Vector3.zero)
        {
            _navMeshAgent.transform.LookAt(new Vector3(_lookAt.x, _lookAt.y, _lookAt.z));
            _lookAt = Vector3.zero;
            ReachedDestiny?.Invoke();
        }
    }

    //protected override void CharacterChanged()
    //{
    //    // Do any circle-specific processing here.

    //    // Call the base class event invocation method.
    //    base.CharacterChanged();
    //}

}
