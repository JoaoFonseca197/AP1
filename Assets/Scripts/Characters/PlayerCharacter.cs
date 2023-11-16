using UnityEngine;


/// <summary>
/// Class responsible for the player character
/// </summary>
public class PlayerCharacter : Characters
{

    private void FixedUpdate()
    {
        
        if ((transform.position - _destiny).magnitude <= _navMeshAgent.stoppingDistance + 0.5f && _isMoving)
        {
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.transform.LookAt(_lookAt);
            _navMeshAgent.updateRotation = true;
            ReachedDestiny?.Invoke();
            _isMoving = false;
        }
        
        
    }


}
