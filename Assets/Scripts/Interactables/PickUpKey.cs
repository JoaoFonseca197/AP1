using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : Interactable
{
    [SerializeField] Transform _pivot;
    //Characters characters;

    public override void Interact(Characters character)
    {

        _currentCharacter = character;
        Destroy(gameObject);
        //Key State of the chracter that caught it is true and can interact with keyhole
        character._hasKey = true;

    }
}
