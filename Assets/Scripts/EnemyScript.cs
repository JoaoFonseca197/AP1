using LibGameAI.FSMs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float                  _maxLookRange;
    [Range(0,360)]
    [SerializeField] float                  _viewAngle;
    [SerializeField] List<Transform>        _patrollingPoints;
    [SerializeField] private NavMeshAgent   _navMeshAgent;
    [SerializeField] private Transform      _player;
    [SerializeField] private Transform      _mason;
    [SerializeField] private LayerMask      _charactersLayerMask;

    private int             _currentPatrollingPoint;
    private Vector3         _characterToChase;
    private StateMachine    _fms;
    private bool            _canSeePlayer;
    private void Awake()
    {
        _currentPatrollingPoint = 0;
    }

    private void Start()
    {
        State patrolling = new State("Patrolling",null,
        ()=> Patrolling(),null);
        State chase = new State("Chasing", null, () => Chase(), null);

        Transition onPatrollingToChase = new Transition(CheckForCharacters,
        null, chase);

        Transition onChaseToPatrol = new Transition(()=>
        (_player.position - transform.position).magnitude > _maxLookRange &&
        (_mason.position - transform.position).magnitude > _maxLookRange,
        null,patrolling);

        patrolling.AddTransition(onPatrollingToChase);
        chase.AddTransition(onChaseToPatrol);

        _fms = _fms = new StateMachine(patrolling);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Characters>())
            other.GetComponent<Characters>().Die();
    }

    private void Update()
    {
        // Get actions from state machine and invoke them
        Action actionToDo = _fms.Update();
        actionToDo?.Invoke();
    }

    /// <summary>
    /// Character will go point to point through all
    /// point in the _patrollingPoints array
    /// </summary>
    private void Patrolling()
    {
        _navMeshAgent.SetDestination(_patrollingPoints[_currentPatrollingPoint].position);
        //variable made to ignore y axis
        Vector3 xzPosition = new Vector3(_patrollingPoints[_currentPatrollingPoint].position.x,
            _navMeshAgent.gameObject.transform.position.y, _patrollingPoints[_currentPatrollingPoint].position.z);
        if (_navMeshAgent.gameObject.transform.position == xzPosition)
        {
           
            _currentPatrollingPoint++;
            if(_currentPatrollingPoint >= _patrollingPoints.Count)
            {
                _currentPatrollingPoint = 0;
            }

        }
    }

    /// <summary>
    /// Gos after the closest character
    /// </summary>
    private void Chase()
    {
        //_characterToChase = ChekForClosestCharacter();
        _navMeshAgent.SetDestination(_characterToChase);
    }

    private bool CheckForCharacters()
    {
        Collider[] character = Physics.OverlapSphere(transform.position, _maxLookRange, _charactersLayerMask);
        List<Vector3> characterPositions = new List<Vector3>();
        
        //See if they are inside of vision range
        if(character.Length != 0)
        {
            //Checks for each character
            for(int i = 0; i < character.Length; i++)
            {
                Vector3 targetDirection = (character[i].transform.position - transform.position).normalized;
                //if they are inside of the cone vision
                if (Vector3.Angle(character[i].transform.forward, targetDirection) < _viewAngle/2)
                {
                    //Change to see obstruction ?
                    if (Physics.Raycast(transform.position, targetDirection, _maxLookRange, _charactersLayerMask))
                        characterPositions.Add(character[i].transform.position);
                }
            }

            if(characterPositions.Count == 0)
            {
                return false;
            }
            else
            {
                _characterToChase = ChekForClosestCharacter(characterPositions);
                return true;
            }
            
        }
        else
            return false;
    }

    /// <summary>
    /// Sees whats is the closest character
    /// </summary>
    /// <returns>The position of the closest character</returns>
    private Vector3 ChekForClosestCharacter(List<Vector3> characters)
    {
        
        if(characters.Count == 1)
            return characters[0];
        else
        {
            float distance1 = Vector3.Distance(transform.position, characters[0]);
            float distance2 = Vector3.Distance(transform.position, characters[1]);
            if (distance1< distance2)
                return characters[0];
            else
                return characters[1];
        }
    }

    
}
