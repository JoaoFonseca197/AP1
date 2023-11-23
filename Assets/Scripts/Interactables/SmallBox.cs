using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SmallBox : Interactable, IInteractable
{
    [SerializeField] Transform _pivot;

    private NavMeshObstacle _navMeshObstacle;
    private float           _initialYposition;
    private Characters      _currentCharacter;
    

    void  Awake()
    {
        base.Awake();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _initialYposition = transform.position.y;
    }

    public override void Interact(Characters character)
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

    public override void StopInteract()
    {

        transform.parent = null;
        transform.SetLocalPositionAndRotation(new Vector3(transform.position.x, _initialYposition, transform.position.z), Quaternion.identity);
        _navMeshObstacle.enabled = true;
        _currentCharacter.Interactable = null;
        _currentCharacter.NavMeshAgent.speed = 10;
        _currentCharacter.NavMeshAgent.angularSpeed = 360;
        _currentCharacter = null;
    }

}
