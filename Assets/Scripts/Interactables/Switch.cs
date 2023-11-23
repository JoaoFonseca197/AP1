using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Switch : Interactable
{
    [SerializeField] private Animator _objectanimator;
    [SerializeField] private Animator _animator;

    private bool _isActive;

    private new void Awake()
    {
        base.Awake();
        _isActive = false; 
    }


    public override void Interact(Characters character)
    {
        if(_isActive)
        {
            _animator.SetBool("IsActive", false);
            _objectanimator.SetBool("IsActive", false);
            _isActive = false;
        }
        else
        {
            _animator.SetBool("IsActive", true);
            _objectanimator.SetBool("IsActive", true);
            _isActive = true;
        }
    }
}
