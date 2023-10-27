using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the Mason character
/// </summary>
public class MasonCharacter : MonoBehaviour , ICharacter
{
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] PlayerCharacter _playerController;

    //Property that tells if this character is following or not
    public bool IsFollowing {get;set;}
    

    
    void FixedUpdate()
    {
        //If true follows the player character
        if (IsFollowing)
            _navMeshAgent.SetDestination(_playerController.gameObject.transform.position);
    }

    /// <summary>
    /// Method used to make the NavMeshAgent of the Mason character move
    /// </summary>
    /// <param name="destiny">Place where the character will go</param>
    public void Move(Vector3 destiny)
    {
        _navMeshAgent.SetDestination(destiny);
    }
}
