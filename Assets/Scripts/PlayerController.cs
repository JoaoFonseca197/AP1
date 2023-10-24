using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] NavMeshAgent _navAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            
            Ray mouseInWorldPOs =_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseInWorldPOs, out RaycastHit hit))
                if(hit.transform.gameObject.layer == 7)
                    _navAgent.SetDestination(hit.point);
        }
            
    }

    private void OnDrawGizmos()
    {
        
    }
}
