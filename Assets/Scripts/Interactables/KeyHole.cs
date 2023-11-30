using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyHole : Interactable
{
    [SerializeField] private Animator _objectanimator;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeToActivateAgain;
    [SerializeField] private GameObject _keyanim;

    public override void Interact(Characters character)
    {
        if (character.HasKey == true)
        {
            _keyanim.SetActive(true);
            _animator.SetBool("IsActive", true);
            _objectanimator.SetBool("IsActive", true);
        }
        
        else
        {
            Console.WriteLine("You don't have a key");
        }
    }


    public override void StopInteract()
    {
        _animator.SetBool("IsActive", false);
        _objectanimator.SetBool("IsActive", false);
    }
}
