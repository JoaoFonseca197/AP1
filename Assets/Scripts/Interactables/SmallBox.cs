using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SmallBox : Interactable
{
    [SerializeField] Transform _pivot;

    private NavMeshObstacle _navMeshObstacle;
    

    private new void  Awake()
    {
        base.Awake();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    public override void Interact(Characters character)
    {

            _interactingCharacter = character;
            transform.SetParent(character.transform, true);
            transform.SetLocalPositionAndRotation(new Vector3(0, 0.5f, transform.localScale.z + 1f), Quaternion.identity);
            _navMeshObstacle.enabled = false;
            _interactingCharacter.Interactable = this;
            _interactingCharacter.NavMeshAgent.speed = 6;
            _interactingCharacter.NavMeshAgent.angularSpeed = 0;

    }

    public override void StopInteract()
    {
        Vector3 distanceToCharacter = _interactingCharacter.transform.forward + _interactingCharacter.transform.position + transform.localPosition;
        transform.parent = null;
        transform.SetLocalPositionAndRotation(new Vector3(distanceToCharacter.x, transform.localScale.y / 2, distanceToCharacter.z), Quaternion.identity);
        _navMeshObstacle.enabled = true;
        _interactingCharacter.Interactable = null;
        _interactingCharacter.NavMeshAgent.speed = 10;
        _interactingCharacter.NavMeshAgent.angularSpeed = 360;
        _interactingCharacter = null;
    }

}
