using UnityEngine;


public class PressurePlate : MonoBehaviour 
{
    [SerializeField] private Animator _objectToActivate;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _collider;
    public Characters CurrentCharacter { get; }

    private bool _isActive = false;
    private void Awake()
    {
        _isActive = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Box>())
        {
            _isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_isActive && other.TryGetComponent<Box>(out Box baba)) 
        {
            _isActive = false;
        }

    }


}
