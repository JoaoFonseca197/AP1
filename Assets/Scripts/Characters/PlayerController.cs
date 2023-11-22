// Ignore Spelling: Interactable

using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class responsible for getting and interpret the inputs from the player
/// </summary>
public class PlayerController: MonoBehaviour 
{

    [SerializeField] protected  Camera              _camera;
    [SerializeField] protected  CinemachineFreeLook _cineCamera;
    [SerializeField] protected  PlayerCharacter     _player;
    [SerializeField] protected  MasonCharacter      _mason;
    [SerializeField] protected  LayerMask           _groundMask;
    [SerializeField] private    LayerMask           _interactableMask;
    [SerializeField] private bool _isInPuzzleRoom;



    private bool                _controllingPlayerCharacter;
    private IInteractable       _currentInteractable;
    private IInteractable       _objectToInteract;
    private Characters          _characterToInteract;
    private Characters          _currentCharacter;
    private List<Renderer>      _currentOutlinedObject;

    public IInteractable CurrentInteractable
    {
        get { return _currentInteractable; }
        set { _currentInteractable = value; }
    }
    

    private void OnEnable()
    {
        _player.ReachedDestiny += Interact;
        _mason.ReachedDestiny += Interact;
    }

    private void OnDisable()
    {
        _mason.ReachedDestiny -= Interact;
        _player.ReachedDestiny -= Interact;
    }

    private void Awake()
    {
        _currentOutlinedObject = new List<Renderer>();
        _currentCharacter = _player;
        _controllingPlayerCharacter = true;
        _isInPuzzleRoom = true;
        _cineCamera.Follow = _player.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        //Shoots a ray from the camera with the direction of the mouse position
        Ray mouseInput = _camera.ScreenPointToRay(Input.mousePosition);

        if(_currentInteractable== null)
        {
            if (Physics.Raycast(mouseInput, out RaycastHit interactable, float.MaxValue, _interactableMask))
            {
                //Goes trough all the meshes of the object and changes the scale of the outline
                _currentOutlinedObject = interactable.collider.GetComponentsInChildren<Renderer>().ToList();
                foreach (Renderer renderer in _currentOutlinedObject)
                    renderer.materials[1].SetFloat("_Scale", 1.1f);
            }
            else
            {
                //Checks if its null
                if (_currentOutlinedObject.Count != 0)
                {
                    //Goes trough all the meshes of the object and changes resets the outline scale
                    foreach (Renderer renderer in _currentOutlinedObject)
                        renderer.materials[1].SetFloat("_Scale", 0f);
                }

            }
        }
        
            

        //Gets input when the player presses Left Mouse Button
        if (Input.GetButtonDown("Fire1"))
        {
            
            //Checks if hits something
            if (Physics.Raycast(mouseInput, out RaycastHit hit, float.MaxValue, _groundMask))
            {

                _currentCharacter.Move(hit.transform, hit.point);

            }

        }
        if (Input.GetButtonDown("Fire2"))
        {
            //Checks if hits something
            if (Physics.Raycast(mouseInput, out RaycastHit hit, float.MaxValue, _interactableMask))
            {
                IInteractable hitInteractable = hit.collider.GetComponent<IInteractable>();
                if (_currentCharacter.Interactable == null && hitInteractable.CurrentCharacter == null)
                {
                    _currentCharacter.Move(hit.transform.position + hit.normal, hit.transform.position);
                    _objectToInteract = hitInteractable;
                    _characterToInteract = _currentCharacter;
                }

                if(_currentCharacter.Interactable == hitInteractable)
                {
                    _currentCharacter.Move(hit.transform.position + hit.normal, hit.transform.position);
                    
                }
            }
        }
        //
        //Test the swaping camera
        //Works better when _isInPuzzleRoom is true
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Checks which character is player controlling
            if(_currentCharacter == _player)
            {
                //Swaps the controlling character to Mason
                _currentCharacter = _mason;
                //Sets the camera to track the Mason character
                _cineCamera.Follow = _mason.transform;
            }
            else
            {
                //Swaps the controlling character to player
                _currentCharacter = _player;
                //Sets the camera to track the player character
                _cineCamera.Follow = _player.transform;
            }
        }
    }



    private void Interact()
    {
        if(_characterToInteract.Interactable == null)
        {
            _currentInteractable = _objectToInteract;
            _objectToInteract = null;
            _currentInteractable?.Interact(_characterToInteract);
        }
        else
        {
            _currentCharacter.Interactable.StopInteract();
        }
        //_currentInteractable = _objectToInteract;
        //_objectToInteract = null;
        //_currentInteractable?.Interact(_currentCharacter); 
    }

}
