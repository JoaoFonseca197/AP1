public class InteractableState : Interactable
{


    // Start is called before the first frame update


    public override  void Interact(Characters character)
    {
        base.Interact(character);
        

        if (_requirementsMet)
        {
            
            IsActive = true;
            _animator.SetBool("IsActive", IsActive);
        }
        else
        {
            IsActive = false;
            _animator.SetBool("IsActive", IsActive);
        }


    }



}
