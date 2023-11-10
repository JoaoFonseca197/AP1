using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] Transform   _pivot;

    private NavMeshObstacle _navMeshObstacle;
    private float _initialYposition;
    private Characters _currentCharacter;

    public Characters CurrentCharacter => _currentCharacter;
    void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _initialYposition = transform.position.y;
    }

    void IInteractable.Interact(Characters character)
    {
        
        if(_currentCharacter == null)
        {
            _currentCharacter = character;
            //Fix position when grabbing
            transform.SetParent(character.transform, true);
            transform.SetLocalPositionAndRotation(new Vector3(0, 0.5f, transform.localScale.z + 1f), Quaternion.identity);
            _navMeshObstacle.enabled = false;
            _currentCharacter.NavMeshAgent.speed = 6;
            _currentCharacter.NavMeshAgent.angularSpeed = 0;
        }
        //else
        //{
        //    transform.parent = null;
        //    transform.SetLocalPositionAndRotation(new Vector3(transform.position.x,_initialYposition, transform.position.z), Quaternion.identity);
        //    _navMeshObstacle.enabled = true;
        //    character.NavMeshAgent.speed = 10;
        //    character.NavMeshAgent.angularSpeed = 360;
        //}   
    }

    void IInteractable.StopInteract()
    {
        transform.parent = null;
        transform.SetLocalPositionAndRotation(new Vector3(transform.position.x, _initialYposition, transform.position.z), Quaternion.identity);
        _navMeshObstacle.enabled = true;
        _currentCharacter.NavMeshAgent.speed = 10;
        _currentCharacter.NavMeshAgent.angularSpeed = 360;
        _currentCharacter = null;
    }


}