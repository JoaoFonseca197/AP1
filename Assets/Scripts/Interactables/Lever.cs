using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator   _objectanimator;
    [SerializeField] private Animator   _animator;
    [SerializeField] private PlayerController _playerController;
    public Characters CurrentCharacter { get; }

    void IInteractable.Interact(Characters character)
    {
        _animator.SetBool("IsActive", true);
        _objectanimator.SetBool("IsActive", true);
        _playerController.CurrentInteractable = null;

    }


    void IInteractable.StopInteract() { }

}
