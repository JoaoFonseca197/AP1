using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the Mason character
/// </summary>
public class MasonCharacter : Characters 
{
    
    [SerializeField] protected PlayerCharacter _playerCharacter;

    //Property that tells if this character is following or not
    public bool IsFollowing {get;set;}
    

    
    void FixedUpdate()
    {
        //If true follows the player character
        if (IsFollowing)
            _navMeshAgent.SetDestination(_playerCharacter.gameObject.transform.position);
    }

}
