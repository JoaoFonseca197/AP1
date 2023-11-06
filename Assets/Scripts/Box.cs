using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _player;
    [SerializeField] Transform   _pivot;
    CharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IInteractable.Interact()
    {
        //Fix position when grabbing
        transform.SetParent(_player.transform,true);
        transform.SetLocalPositionAndRotation(new Vector3 (0,0.5f,  transform.localScale.z + 1f), Quaternion.identity);
        //transform.rotation = Quaternion.EulerRotation(Vector3.zero);
        
        //navMeshAgent.angularSpeed = 0;
        
            
    }

    
}
