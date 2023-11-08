using UnityEngine;
using UnityEngine.AI;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerCharacter _player;
    [SerializeField] Transform   _pivot;

    private NavMeshObstacle _navMeshObstacle;
    private float _initialYposition;
    private 
    void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _initialYposition = transform.position.y;
    }

    void IInteractable.Interact(bool isInteracting)
    {
        if(isInteracting)
        {
            //Fix position when grabbing
            transform.SetParent(_player.transform, true);
            transform.SetLocalPositionAndRotation(new Vector3(0, 0.5f, transform.localScale.z + 1f), Quaternion.identity);
            _navMeshObstacle.enabled = false;
            _player.NavMeshAgent.speed = 6;
            _player.NavMeshAgent.angularSpeed = 0;

        }
        else
        {
            transform.parent = null;
            transform.SetLocalPositionAndRotation(new Vector3(transform.position.x,_initialYposition, transform.position.z), Quaternion.identity);
            _navMeshObstacle.enabled = true;
            _player.NavMeshAgent.speed = 10;
            _player.NavMeshAgent.angularSpeed = 360;
        }   
    }

    
}
