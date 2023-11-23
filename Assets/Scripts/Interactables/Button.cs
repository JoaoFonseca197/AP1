using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Button : Interactable
{
    [SerializeField] private Animator   _objectanimator;
    [SerializeField] private Animator   _animator;
    [SerializeField] private float      _timeToActivateAgain;

    public override void Interact(Characters character)
    {
        _animator.SetBool("IsActive", true);
        _objectanimator.SetBool("IsActive", true);


        Invoke("StopInteract", _timeToActivateAgain);
    }


    public override void StopInteract()
    {
        _animator.SetBool("IsActive", false);
        _objectanimator.SetBool("IsActive", false);
    }

}
