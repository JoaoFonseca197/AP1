using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _objectanimator;
    [SerializeField] private Animator _animator;

    private Characters _currentCharacter;

    public Characters CurrentCharacter => _currentCharacter;

    void IInteractable.Interact(Characters character)
    {
        _currentCharacter = character;
        _animator.SetBool("IsActive", true);
        _objectanimator.SetBool("IsActive", true);

        _currentCharacter.Interactable = this;
    }


    void IInteractable.StopInteract() 
    {
        _animator.SetBool("IsActive", false);
        _objectanimator.SetBool("IsActive", false);
        _currentCharacter.Interactable = null;
    }
}
