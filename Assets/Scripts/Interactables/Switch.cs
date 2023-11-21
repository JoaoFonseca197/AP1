using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _objectanimator;
    [SerializeField] private Animator _animator;

    private Characters _currentCharacter;
    private bool _isActive;

    private void Awake()
    {
        _isActive = false; 
    }

    public Characters CurrentCharacter => _currentCharacter;

    public void Interact(Characters character)
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


    public void StopInteract() { }
}
