using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator   _objectanimator;
    [SerializeField] private Animator   _animator;
    [SerializeField] private float      _timeToActivateAgain;

    private Characters _currentCharacter;
    private List<Renderer> _renderers;

    public Characters CurrentCharacter => _currentCharacter;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>().ToList();
    }

    public void Interact(Characters character)
    {
        _animator.SetBool("IsActive", true);
        _objectanimator.SetBool("IsActive", true);


        Invoke("StopInteract", _timeToActivateAgain);
    }


    public void StopInteract()
    {
        _animator.SetBool("IsActive", false);
        _objectanimator.SetBool("IsActive", false);
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
