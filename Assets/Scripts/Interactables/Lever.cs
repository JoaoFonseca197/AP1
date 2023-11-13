using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _objectToActivate;
    [SerializeField] private Animator   _animator;
    public Characters CurrentCharacter { get; }

    void IInteractable.Interact(Characters character)
    {
        _animator.Play("Pull_Down");
        
    }


    void IInteractable.StopInteract() { }

}
