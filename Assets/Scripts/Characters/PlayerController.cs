// Ignore Spelling: Interactable

using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class responsible for getting and interpret the inputs from the player
/// </summary>
public class PlayerController: MonoBehaviour 
{
        
    [SerializeField] private Camera              _camera;
    [SerializeField] private CinemachineFreeLook _cineCamera;
    [SerializeField] private PlayerCharacter     _player;
    [SerializeField] private MasonCharacter      _mason;
    [SerializeField] private LayerMask           _groundMask;
    [SerializeField] private LayerMask           _interactableMask;
    [SerializeField] private ParticleSystem      _particle;


    private Characters          _currentCharacter;



    public  Action<Characters> SwitchCharacter;



    

    private void Awake()
    {
        
        _currentCharacter = _player;
        _cineCamera.Follow = _player.transform;
    }

    private void Start()
    {
        SwitchCharacter.Invoke(_currentCharacter);
    }

    // Update is called once per frame
    private void Update()
    {
        //Shoots a ray from the camera with the direction of the mouse position
        Ray mouseInput = _camera.ScreenPointToRay(Input.mousePosition);
        //Gets the value of the Mouse ScrollWheel
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        

        //Gets input when the player presses Left Mouse Button
        if (Input.GetButtonDown("Fire1"))
        {
            
            //Checks if hits something
            if (Physics.Raycast(mouseInput, out RaycastHit hit, float.MaxValue, _groundMask))
            {

                _currentCharacter.Move(hit.point);
                _particle.Play();
                _particle.transform.position = hit.point;

            }

        }
        if (Input.GetButtonDown("Fire2"))
        {
            if(_currentCharacter.Interactable is BigBox || _currentCharacter.Interactable is SmallBox)
            {
                _currentCharacter.Interactable.StopInteract();
                return;
            }
            //Checks if hits something
            if (Physics.Raycast(mouseInput, out RaycastHit hit, float.MaxValue, _interactableMask))
            {
                Interactable hitInteractable = hit.collider.GetComponent<Interactable>();
                if (_currentCharacter.Interactable == null && hitInteractable.InteractingCharacter == null)
                {
                    _currentCharacter.Move(hit.transform.position + hit.normal *2 ,hitInteractable, hit.transform.position);
                }

                if(_currentCharacter.Interactable == hitInteractable)
                {
                    _currentCharacter.Move(hit.transform.position + hit.normal, hitInteractable, hit.transform.position);
                }
            }
        }
        //
        //Test the swaping camera
        //Works better when _isInPuzzleRoom is true
        //float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue != 0f)
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

            SwitchCharacter.Invoke(_currentCharacter);
        }
    }






}
