using LibGameAI.FSMs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float                  _maxLookRange;
    [SerializeField] List<Transform>        _patrollingPoints;
    [SerializeField] private NavMeshAgent   _navMeshAgent;
    [SerializeField] private Transform          _player;
    [SerializeField] private Transform          _mason;

    private int         _currentPatrollingPoint;
    private Vector3     _characterToChase;
    private StateMachine _fms;

    private void Awake()
    {
        _currentPatrollingPoint = 0;
    }
    private void Start()
    {
        State patrolling = new State("Patrolling",null,
        ()=> Patrolling(),null);
        State chase = new State("Chasing", null, () => Chase(), null);

        Transition onPatrollingToChase = new Transition(() =>
        (_player.position - transform.position).magnitude < _maxLookRange ||
        (_mason.position - transform.position).magnitude < _maxLookRange,
        null, chase);

        Transition onChaseToPatrol = new Transition(()=>
        (_player.position - transform.position).magnitude > _maxLookRange &&
        (_mason.position - transform.position).magnitude > _maxLookRange,
        null,patrolling);

        patrolling.AddTransition(onPatrollingToChase);
        chase.AddTransition(onChaseToPatrol);

        _fms = _fms = new StateMachine(patrolling);


    }

    private void Update()
    {
        // Get actions from state machine and invoke them
        Action actionToDo = _fms.Update();
        actionToDo?.Invoke();


        print(_navMeshAgent.destination);
        //Updates the cooldown of shooting
    }


    private void Patrolling()
    {
        _navMeshAgent.SetDestination(_patrollingPoints[_currentPatrollingPoint].position);
        //variable made to ignore y axis
        Vector3 xzPosition = new Vector3(_patrollingPoints[_currentPatrollingPoint].position.x,
            _navMeshAgent.destination.y, _patrollingPoints[_currentPatrollingPoint].position.z);
        if (_navMeshAgent.destination == xzPosition)
        {
           
            _currentPatrollingPoint++;
            if(_currentPatrollingPoint >= _patrollingPoints.Count)
            {
                _currentPatrollingPoint = 0;
            }

        }
    }

    private void Chase()
    {
        _characterToChase = ChekForClosestCharacter();
        _navMeshAgent.SetDestination(_characterToChase);
    }

    private Vector3 ChekForClosestCharacter()
    {
        float enemyToPlayer = (_player.position - transform.position).magnitude;
        float enemyToMason = (_mason.position - transform.position).magnitude;

        if (enemyToMason < enemyToPlayer)
            return _mason.position;
        else
            return _player.position;
    }
}
