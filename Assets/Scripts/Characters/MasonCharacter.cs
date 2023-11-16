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

        if ((transform.position - _destiny).magnitude <= _navMeshAgent.stoppingDistance + 0.5f && _isMoving)
        {
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.transform.LookAt(_lookAt);
            _navMeshAgent.updateRotation = true;
            ReachedDestiny?.Invoke();
            _isMoving = false;
        }
    }

    //protected override void CharacterChanged()
    //{
    //    // Do any circle-specific processing here.

    //    // Call the base class event invocation method.
    //    base.CharacterChanged();
    //}

}
