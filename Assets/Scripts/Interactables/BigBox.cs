using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBox : Interactable
{
    [SerializeField] Transform   _pivot;

    private NavMeshObstacle _navMeshObstacle;

    private new void Awake()
    {
        base.Awake();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    public override void Interact(Characters character)
    {
        
        if(character is PlayerCharacter)
        {
            _interactingCharacter = character;
            transform.SetParent(character.transform, true);
            transform.SetLocalPositionAndRotation(new Vector3(0, 0.5f, transform.localScale.z + 1f), Quaternion.identity);
            _navMeshObstacle.enabled = false;
            _interactingCharacter.Interactable = this;
            _interactingCharacter.NavMeshAgent.speed = 6;
            _interactingCharacter.NavMeshAgent.angularSpeed = 0;
        }  
    }

    public override void StopInteract()
    {

        transform.parent = null;
        transform.rotation = Quaternion.identity;
        _navMeshObstacle.enabled = true;
        _interactingCharacter.Interactable = null;
        _interactingCharacter.NavMeshAgent.speed = 10;
        _interactingCharacter.NavMeshAgent.angularSpeed = 360;
        _interactingCharacter = null;
        
        
    }

}
