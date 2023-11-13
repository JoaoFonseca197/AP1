using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator   _objectanimator;
    [SerializeField] private Animator   _animator;
    public Characters CurrentCharacter { get; }

    void IInteractable.Interact(Characters character)
    {
        _animator.SetBool("_IsActive", true);

    }


    void IInteractable.StopInteract() { }

}
