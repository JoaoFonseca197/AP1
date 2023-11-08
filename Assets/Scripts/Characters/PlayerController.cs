using Cinemachine;
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


    //Can this be used ?
    //private ICharacter              _character;

    private bool            _controllingPlayerCharacter;
    private IInteractable   _interactable;
    private IInteractable   _objectToInteract;

    //Serialized for best test purposes 
    //Remove after
    [SerializeField] private bool _isInPuzzleRoom;

    private void OnEnable()
    {
        _player.ReachedDestiny += Interact;
    }

    private void OnDisable()
    {
        _player.ReachedDestiny -= Interact;
    }

    private void Awake()
    {
        _controllingPlayerCharacter = true;
        _isInPuzzleRoom = true;
    }

    // Update is called once per frame
    private void Update()
    {

        Debug.Log(_interactable);
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
                        _player.Move(hit.point);
                    }
                    else
                    {
                        //Moves Mason character
                        _mason.Move(hit.point);
                    }
                }
                //If is not in the puzzle room 
                //The player character moves and the Mason character follows
                else
                {
                    _cineCamera.Follow = _player.transform;
                    _mason.IsFollowing = true;
                    _player.Move(hit.point,Vector3.zero);
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
                if (_interactable != null)
                {
                    StopInteracting();
                    _interactable = null;
                }
                else
                {
                    //Debug.Log($"Normal={hit.normal}");
                    if (hit.normal.y == 0)
                    {
                        _player.Move(
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
            if(_controllingPlayerCharacter)
            {
                //Swaps the controlling character to Mason
                _controllingPlayerCharacter = false;
                //Sets the camera to track the Mason character
                _cineCamera.Follow = _mason.transform;
            }
            else
            {
                //Swaps the controlling character to player
                _controllingPlayerCharacter = true;
                //Sets the camera to track the player character
                _cineCamera.Follow = _player.transform;
            }
        }
    }

    private void StopInteracting()
    {
        _interactable.Interact(false);
    }

    private void Interact()
    {
        _interactable = _objectToInteract;
        _objectToInteract = null;
        _interactable?.Interact(true); 
    }

}
