using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour , IInteractable
{
    [SerializeField] private GameObject _objectToActivate;
    [SerializeField] private Animator _animator;


    public Characters CurrentCharacter { get; }
    void IInteractable.Interact(Characters character)
    {

        _animator.parameters.SetValue(true,0);

    }

    void IInteractable.StopInteract() { }

}
