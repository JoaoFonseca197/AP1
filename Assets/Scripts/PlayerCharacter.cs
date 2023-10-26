using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the player character
/// </summary>
public class PlayerCharacter : MonoBehaviour, ICharacter
{
    [SerializeField] NavMeshAgent _navMeshAgent;

    /// <summary>
    /// Method used to make the NavMeshAgent of the player character move
    /// </summary>
    /// <param name="destiny">Place where the character will go</param>
    public void Move(Vector3 destiny)
    { 
                _navMeshAgent.SetDestination(destiny);
    }
}
