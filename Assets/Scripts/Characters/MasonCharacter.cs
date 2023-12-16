using UnityEngine;


/// <summary>
/// Class responsible for the Mason character
/// </summary>
public class MasonCharacter : Characters 
{
    [SerializeField] private float _maxSoloHigh;
    [SerializeField] private PlayerCharacter _playerCharacter;


    public override void Move( Vector3 destiny)
    {

        if (Mathf.Abs(destiny.y - _playerPivot.position.y) >= _minHighDistance && Mathf.Abs(destiny.y - _playerPivot.position.y) <= _maxHighDistance)
        {
            if (Mathf.Abs(destiny.y - _playerPivot.position.y)<= _maxSoloHigh)
                base.Move(destiny);
            else
            {
                if(CheckForHelp(destiny))
                {
                    base.Move(destiny);
                }

            }
                    
        }
        else
            _navMeshAgent.SetDestination(destiny);
        
    }

    private bool CheckForHelp(Vector3 destiny)
    {
        float playerToObjectDif = Vector3.Distance(_playerCharacter.transform.position, transform.position);
        if (_playerCharacter.Interactable != null) 
            return false;
        if(playerToObjectDif > 2f)
            return false;
        else
            return true;

    }

}
