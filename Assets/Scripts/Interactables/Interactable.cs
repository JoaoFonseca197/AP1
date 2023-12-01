using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Interactable : MonoBehaviour , IInteractable
{
    [SerializeField] protected List<Interactable> _requirements;
    [SerializeField] protected Interactable _objectToActivate;
    [SerializeField] protected Animator _animator;


    private List<Renderer>  _renderers;

    protected Characters _currentCharacter;

    protected bool _requirementsMet;

    [field: SerializeField]
    protected bool IsActive { get;set; }

    
    public Characters CurrentCharacter => _currentCharacter;

    protected  void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>().ToList();
    }
    public virtual void Interact(Characters character) 
    {
        if(_requirements.Count > 0)
        {
            foreach (Interactable interactable in _requirements)
            {
                if (!interactable.IsActive)
                {
                    _requirementsMet = false;
                    return;
                }
                _requirementsMet = true;
            }
        }
        _requirementsMet = true;
        
    }
    

    public virtual void StopInteract() { }

    protected virtual void OnMouseEnter()
    {
        List<Renderer> renderers = GetComponentsInChildren<Renderer>().ToList();
        foreach (Renderer renderer in renderers)
            renderer.materials[1].SetFloat("_Scale", 1.1f);
    }

    protected virtual void OnMouseExit()
    {
        foreach (Renderer renderer in _renderers)
            renderer.materials[1].SetFloat("_Scale", 0f);
    }

}
