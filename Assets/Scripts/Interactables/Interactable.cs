using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Interactable : MonoBehaviour , IInteractable
{
    private List<Renderer> _renderers;

    // Start is called before the first frame update
    private Characters _currentCharacter;

    
    public Characters CurrentCharacter => _currentCharacter;

    protected  void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>().ToList();
    }
    public virtual void Interact(Characters character) { }
    

    public virtual void StopInteract() { }

    protected void OnMouseEnter()
    {
        List<Renderer> renderers = GetComponentsInChildren<Renderer>().ToList();
        foreach (Renderer renderer in renderers)
            renderer.materials[1].SetFloat("_Scale", 1.1f);
    }

    protected void OnMouseExit()
    {
        foreach (Renderer renderer in _renderers)
            renderer.materials[1].SetFloat("_Scale", 0f);
    }

}
