using UnityEngine;

public class Button : Interactable
{
    [SerializeField] private float      _timeToActivateAgain;

    public override void Interact(Characters character)
    {
        base.Interact(character);

        if (_requirementsMet)
        {
            IsActive = true;
            _animator.SetBool("IsActive", IsActive);
            _objectToActivate.Interact(character);
            Invoke("StopInteract", _timeToActivateAgain);
        }
    }


    public override void StopInteract()
    {
        IsActive= false;
        _animator.SetBool("IsActive", IsActive);
        _objectToActivate.Interact(_currentCharacter);
    }

}
