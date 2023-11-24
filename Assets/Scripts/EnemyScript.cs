using LibGameAI.FSMs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public float                  _maxLookRange;
    [Range(0,360)]
    [SerializeField] public float                  _viewAngle;
    [SerializeField] List<Transform>        _patrollingPoints;
    [SerializeField] private NavMeshAgent   _navMeshAgent;
    [SerializeField] private Transform      _player;
    [SerializeField] private Transform      _mason;
    [SerializeField] private LayerMask      _charactersLayerMask;

    private bool                _canSeeCharacter;
    private int                 _currentPatrollingPoint;
    private Vector3             _characterToChase;
    private StateMachine        _fms;
    private List<Transform>     _charactersToChase;
    private void Awake()
    {
        _currentPatrollingPoint = 0;
        _charactersToChase = new List<Transform>();
    }

    private void Start()
    {
        State patrolling = new State("Patrolling",null,
        ()=> Patrolling(),null);
        State chase = new State("Chasing", null, () => Chase(), null);

        Transition onPatrollingToChase = new Transition(CheckForCharacters,
        null, chase);

        Transition onChaseToPatrol = new Transition(()=> !CheckForCharacters(),
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
        print("Patrolling");
        _navMeshAgent.SetDestination(_patrollingPoints[_currentPatrollingPoint].position);
        //variable made to ignore y axis
        Vector3 xzPosition = new Vector3(_patrollingPoints[_currentPatrollingPoint].position.x,
            _navMeshAgent.gameObject.transform.position.y, _patrollingPoints[_currentPatrollingPoint].position.z);

        float distanceToPatrolP = Vector3.Distance(transform.position, xzPosition);
        if (distanceToPatrolP < 1f)
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
        print("Chasing");
        _characterToChase = CheckForClosestCharacter(_charactersToChase);
        _navMeshAgent.SetDestination(_characterToChase);
    }

    private bool CheckForCharacters()
    {
        Collider[] character = Physics.OverlapSphere(transform.position, _maxLookRange, _charactersLayerMask);
        
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
                        _charactersToChase.Add(character[i].transform);
                }
            }

            if(_charactersToChase.Count == 0)
            {
                return false;
            }
            else
            {

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
    private Vector3 CheckForClosestCharacter(List<Transform> characters)
    {
        
        if(characters.Count == 1)
            return characters[0].position;
        else
        {
            float distance1 = Vector3.Distance(transform.position, characters[0].position);
            float distance2 = Vector3.Distance(transform.position, characters[1].position);
            if (distance1< distance2)
                return characters[0].position;
            else
                return characters[1].position;
        }
    }

    
}
