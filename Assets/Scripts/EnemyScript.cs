using LibGameAI.FSMs;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

/// <summary>
/// Class responsible for the enemy AI behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
    [SerializeField] public float                   _maxLookRange;
    [Range(0,360)]
    [SerializeField] public float                   _viewAngle;
    [SerializeField] List<Transform>                _patrollingPoints;
    [SerializeField] private NavMeshAgent           _navMeshAgent;
    [SerializeField] private LayerMask              _charactersLayerMask;
    [SerializeField] private LayerMask              _obstructionMash;
    [SerializeField] private LayerMask              _interactableMash;
    [SerializeField] private TagAttribute           _player;

    private bool                _seesPlayer;
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

        Transition onPatrollingToChase = new Transition(()=> _seesPlayer,
        null, chase);

        Transition onChaseToPatrol = new Transition(()=> !_seesPlayer,
        null,patrolling);

        patrolling.AddTransition(onPatrollingToChase);
        chase.AddTransition(onChaseToPatrol);

        _fms = _fms = new StateMachine(patrolling);


    }

    /// <summary>
    /// If touches another character kills it
    /// </summary>
    /// <param name="other">Collider that hit this one</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Characters>())
            other.GetComponent<Characters>().Die();
    }

    private void Update()
    {
        CheckForCharacters();
        // Get actions from state machine and invoke them
        Action actionToDo = _fms.Update();

        actionToDo?.Invoke();
    }

    /// <summary>
    /// Character will go point to point through all
    /// point in the _patrollingPoints array
    /// If there is no patrol points the enemy will
    /// stand in one place
    /// </summary>
    private void Patrolling()
    {

        //If there is no points the enemy will stand in one place
        if (_patrollingPoints.Count == 0)
            return;

        //Sets the destination of the enemy to the current PatrolPoint
        _navMeshAgent.SetDestination(_patrollingPoints[_currentPatrollingPoint].position);
        //variable made to ignore y axis
        Vector3 xzPosition = new Vector3(_patrollingPoints[_currentPatrollingPoint].position.x,
            _navMeshAgent.gameObject.transform.position.y, _patrollingPoints[_currentPatrollingPoint].position.z);
        //Checks the distance to the PatrolPoint
        float distanceToPatrolP = Vector3.Distance(transform.position, xzPosition);

        //If its lesser 
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
        _characterToChase = CheckForClosestCharacter(_charactersToChase);
        _charactersToChase.Clear();
        _navMeshAgent.SetDestination(_characterToChase);
    }

    private void CheckForCharacters()
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
                if (Vector3.Angle(transform.forward, targetDirection) < _viewAngle/2)
                {
                    Ray ray = new Ray(transform.position,targetDirection);
                    //Sees if is not an obstruction or interactable
                    if (!Physics.Raycast(ray,  _maxLookRange, _obstructionMash) &&
                        !Physics.Raycast(ray, _maxLookRange, _interactableMash))
                    {
                            _charactersToChase.Add(character[i].transform);

                    }
                        
                }
            }

            if(_charactersToChase.Count == 0)
            {
                _seesPlayer = false;
            }
            else
            {

                _seesPlayer = true;
            }
            
        }
        else
            _seesPlayer = false;
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
