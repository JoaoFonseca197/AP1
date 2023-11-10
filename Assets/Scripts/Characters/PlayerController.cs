using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

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


    //Can this be used ?
    //private ICharacter              _character;

    private bool            _controllingPlayerCharacter;
    private IInteractable   _interactable;
    private IInteractable   _objectToInteract;
    private Characters      _currentCharacter;

    //Serialized for best test purposes 
    //Remove after
    [SerializeField] private bool _isInPuzzleRoom;

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
        _currentCharacter = _player;
        _controllingPlayerCharacter = true;
        _isInPuzzleRoom = true;
    }

    // Update is called once per frame
    private void Update()
    {


        //Gets input when the player presses Left Mouse Button
        if (Input.GetButtonDown("Fire1"))
        {
            //Shoots a ray from the camera with the direction of the mouse position
            Ray mouseInput = _camera.ScreenPointToRay(Input.mousePosition);
            //Checks if hits something
            if (Physics.Raycast(mouseInput, out RaycastHit hit, float.MaxValue, _groundMask))
            {
                //NOTE: May change to navMesh Ray
                //Checks if the characters are in the puzzle room
                
                if (_isInPuzzleRoom)
                {
                    //Character Mason stops following player character
                    _mason.IsFollowing = false;
                    //Checks which character is the player controlling
                    //If true the player is controlling the player character
                    if (_controllingPlayerCharacter)
                    {
                        //Moves player character
                        _currentCharacter.Move(hit.point);
                        //_currentCharacter = _player;
                    }
                    else
                    {
                        //Moves Mason character
                        _currentCharacter.Move(hit.point);
                        //_currentCharacter = _mason;
                    }
                }
                //If is not in the puzzle room 
                //The player character moves and the Mason character follows
                else
                {
                    _cineCamera.Follow = _player.transform;
                    _mason.IsFollowing = true;
                    _player.Move(hit.point,Vector3.zero);
                    _currentCharacter = _player;
                }
            }

        }
        if (Input.GetButtonDown("Fire2"))
        {
            //Shoots a ray from the camera with the direction of the mouse position
            Ray mouseInput = _camera.ScreenPointToRay(Input.mousePosition);
            //Checks if hits something
            if (Physics.Raycast(mouseInput, out RaycastHit hit, float.MaxValue, _interactableMask))
            {
                //Stops interacting with the object
                if (_interactable != null && _currentCharacter == _interactable.CurrentCharacter)
                {
                    _interactable.StopInteract();
                    _interactable = null;
                }
                else
                {
                    //Debug.Log($"Normal={hit.normal}");
                    if (hit.normal.y == 0)
                    {
                        _currentCharacter.Move(
                            new Vector3(hit.transform.position.x, 0, hit.transform.position.z - hit.transform.localScale.z + hit.normal.z),
                            hit.transform.position);
                        _objectToInteract = hit.collider.GetComponent<IInteractable>();

                    }
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
        _interactable = _objectToInteract;
        _objectToInteract = null;
        _interactable?.Interact(_currentCharacter); 
    }

}
