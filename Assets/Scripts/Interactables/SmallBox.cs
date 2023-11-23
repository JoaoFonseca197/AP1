using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SmallBox : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _pivot;

    private NavMeshObstacle _navMeshObstacle;
    private float           _initialYposition;
    private Characters      _currentCharacter;
    private List<Renderer>  _renderers;

    public Characters CurrentCharacter => _currentCharacter;
    void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _initialYposition = transform.position.y;
        _renderers =  GetComponentsInChildren<Renderer>().ToList();
    }

    public void Interact(Characters character)
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

    public void StopInteract()
    {

        transform.parent = null;
        transform.SetLocalPositionAndRotation(new Vector3(transform.position.x, _initialYposition, transform.position.z), Quaternion.identity);
        _navMeshObstacle.enabled = true;
        _currentCharacter.Interactable = null;
        _currentCharacter.NavMeshAgent.speed = 10;
        _currentCharacter.NavMeshAgent.angularSpeed = 360;
        _currentCharacter = null;
    }

    private void OnMouseEnter()
    {
        List<Renderer> renderers = GetComponentsInChildren<Renderer>().ToList();
        foreach (Renderer renderer in renderers)
            renderer.materials[1].SetFloat("_Scale", 1.1f);
    }

    private void OnMouseExit()
    {
        foreach (Renderer renderer in _renderers)
            renderer.materials[1].SetFloat("_Scale", 0f);
    }
}
