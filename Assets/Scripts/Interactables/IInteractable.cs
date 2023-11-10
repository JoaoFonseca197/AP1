// Ignore Spelling: Interactable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{


    public Characters CurrentCharacter { get; }
    void Interact(Characters character);


    void StopInteract();
}
