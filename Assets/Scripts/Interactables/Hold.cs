using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hold : Interactable
{
    [SerializeField] private Animator   _objectanimator;
    [SerializeField] private Animator   _animator;
    [SerializeField] private PlayerController _playerController;


    public override void Interact(Characters character)
    {
        _animator.SetBool("IsActive", true);
        _objectanimator.SetBool("IsActive", true);
        _playerController.CurrentInteractable = null;
        _currentCharacter = character;
        _currentCharacter.NavMeshAgent.isStopped = true;
        _currentCharacter.Interactable = this;
        
    }


    public override void StopInteract() 
    {
        _animator.SetBool("IsActive", false);
        _objectanimator.SetBool("IsActive", false);
        _currentCharacter.Interactable = null;
        _currentCharacter.NavMeshAgent.isStopped = false;
        _currentCharacter = null;
    }

}
