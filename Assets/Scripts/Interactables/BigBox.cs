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
            _currentCharacter = character;
            transform.SetParent(character.transform, true);
            transform.SetLocalPositionAndRotation(new Vector3(0, 0.5f, transform.localScale.z + 1f), Quaternion.identity);
            _navMeshObstacle.enabled = false;
            _currentCharacter.Interactable = this;
            _currentCharacter.NavMeshAgent.speed = 6;
            _currentCharacter.NavMeshAgent.angularSpeed = 0;
            _currentCharacter.transform.rotation = Quaternion.identity;
        }  
    }

    public override void StopInteract()
    {
        Vector3 distanceToCharacter = _currentCharacter.transform.position + transform.localPosition;
        transform.parent = null;
        transform.SetLocalPositionAndRotation(new Vector3(distanceToCharacter.x, transform.localScale.y / 2, distanceToCharacter.z), Quaternion.identity);
        _navMeshObstacle.enabled = true;
        _currentCharacter.Interactable = null;
        _currentCharacter.NavMeshAgent.speed = 10;
        _currentCharacter.NavMeshAgent.angularSpeed = 360;
        _currentCharacter = null;
    }

}
