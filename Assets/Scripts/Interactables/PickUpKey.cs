using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : Interactable
{
    [SerializeField] Transform _pivot;

    public override void Interact(Characters character)
    {

        _interactingCharacter = character;
        Destroy(gameObject);
        //Key State of the chracter that caught it is true and can interact with keyhole
        character.HasKey = true;

    }
}
