using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator   _objectanimator;
    [SerializeField] private Animator   _animator;
    [SerializeField] private float      _timeToActivateAgain;

    private Characters _currentCharacter;

    public Characters CurrentCharacter => _currentCharacter;

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
}
