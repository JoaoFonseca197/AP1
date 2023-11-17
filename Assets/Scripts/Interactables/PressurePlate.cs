using UnityEngine;


public class PressurePlate : MonoBehaviour 
{
    [SerializeField] private Animator _objectToActivate;
    [SerializeField] private Animator _animator;
    public Characters CurrentCharacter { get; }

    private bool _isActive = false;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Box>())
        {
            _animator.SetBool("IsActive", true);
            _objectToActivate.SetBool("IsActive", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_isActive && other.TryGetComponent<Box>(out Box baba)) 
        {
            _animator.SetBool("IsActive", false);
            _objectToActivate.SetBool("IsActive", false);
        }

    }


}
