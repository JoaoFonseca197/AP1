using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _objectToActivate;
    [SerializeField] private Animator   _animator;
    public Characters CurrentCharacter { get; }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void IInteractable.Interact(Characters character)
    {

    }

    private void ObjectActivate()
    {

    }

    void IInteractable.StopInteract() { }

    // Update is called once per frame
    void Update()
    {
        
    }
}
