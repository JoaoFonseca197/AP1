// Ignore Spelling: interactable

using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the player character
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navMeshAgent;

    private Vector3         _lookAt;
    private IInteractable   _interactable;

    /// <summary>
    /// Method used to make the NavMeshAgent of the player character move
    /// </summary>
    /// <param name="destiny">Place where the character will go</param>
    public void Move(Vector3 destiny)
    { 
        
        _navMeshAgent.SetDestination(destiny);

    }

    public void Move(Vector3 destiny, Vector3 lookAt, IInteractable  interactable)
    {

        _navMeshAgent.SetDestination(destiny);
        _lookAt = lookAt;
        _interactable = interactable;
    }
    /// <summary>
    /// In future can be used to rotate ?
    /// </summary>
    //private IEnumerator RotateToObject()
    //{
    //    while(_navMeshAgent.velocity.magnitude>0 && _lookAt != Vector3.zero)
    //    {
    //        yield return null;
    //    }

    //    transform.Rotate(_lookAt);
    //    _lookAt = Vector3.zero;
    //}



    private void FixedUpdate()
    {
        if (_navMeshAgent.remainingDistance <= 0 + _navMeshAgent.stoppingDistance && _lookAt != Vector3.zero)
        {
            _navMeshAgent.transform.LookAt(new Vector3(_lookAt.x,transform.rotation.y,_lookAt.z));
            _lookAt = Vector3.zero;
            _interactable.Interact();
            

        }
    }
}
