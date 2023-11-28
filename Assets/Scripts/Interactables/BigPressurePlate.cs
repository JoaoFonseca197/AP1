using System.Collections.Generic;
using UnityEngine;


public class BigPressurePlate : MonoBehaviour 
{
    [SerializeField] private Animator _objectToActivate;
    [SerializeField] private Animator _animator;
    public Characters CurrentCharacter { get; }

    private bool _isActive;
    private GameObject _currentObjectActivating;

    private void Awake()
    {
        _isActive = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BigBox>(out BigBox bigBox))
            _currentObjectActivating = bigBox.gameObject;
        else if (other.TryGetComponent<PlayerCharacter>(out PlayerCharacter playerCharacter))
            _currentObjectActivating = playerCharacter.gameObject;

            if (_currentObjectActivating != null )
            {
                _animator.SetBool("IsActive", true);
                _objectToActivate.SetBool("IsActive", true);
                _isActive = true;
            }

        
    }


    private void OnTriggerExit(Collider other)
    {
        if(_isActive && other.gameObject == _currentObjectActivating) 
        {
            _animator.SetBool("IsActive", false);
            _objectToActivate.SetBool("IsActive", false);
            _isActive = false;
            _currentObjectActivating = null;
        }

    }


}
