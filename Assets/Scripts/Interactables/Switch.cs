
public class Switch : Interactable
{

    

    private new void Awake()
    {
        base.Awake();
    }


    public override void Interact(Characters character)
    {
        base.Interact(character);
        

        if (_requirementsMet)
        {
            if (IsActive)
            {
                IsActive = false;
                _animator.SetBool("IsActive", IsActive);
                _objectToActivate.Interact(character);
            }
            else
            {
                IsActive = true;
                _animator.SetBool("IsActive", IsActive);
                _objectToActivate.Interact(character);
            }
        }
    }
}
