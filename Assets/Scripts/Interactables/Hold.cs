using UnityEngine;


public class Hold : Interactable
{
    [SerializeField] private PlayerController _playerController;


    public override void Interact(Characters character)
    {
        base.Interact(character);
        if(_requirementsMet)
        {
            IsActive = true;
            _animator.SetBool("IsActive", IsActive);
            _objectToActivate.Interact(character);
            _playerController.CurrentInteractable = null;
            _currentCharacter = character;
            _currentCharacter.NavMeshAgent.isStopped = true;
            _currentCharacter.Interactable = this;
        }
    }


    public override void StopInteract() 
    {
        IsActive = false;
        _animator.SetBool("IsActive", IsActive);
        _objectToActivate.Interact(_currentCharacter);
        _currentCharacter.Interactable = null;
        _currentCharacter.NavMeshAgent.isStopped = false;
        _currentCharacter = null;
    }

}
