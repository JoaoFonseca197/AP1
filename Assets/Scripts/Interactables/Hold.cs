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
            _interactingCharacter = character;
            _interactingCharacter.NavMeshAgent.isStopped = true;
            _interactingCharacter.Interactable = this;
        }
    }


    public override void StopInteract() 
    {
        IsActive = false;
        _animator.SetBool("IsActive", IsActive);
        _objectToActivate.Interact(_interactingCharacter);
        _interactingCharacter.Interactable = null;
        _interactingCharacter.NavMeshAgent.isStopped = false;
        _interactingCharacter = null;
    }

}
