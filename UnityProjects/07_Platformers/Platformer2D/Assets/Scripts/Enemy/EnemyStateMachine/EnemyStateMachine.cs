using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : IStateChanger
{
    private readonly Dictionary<Type, EnemyState> _states = new();

    private EnemyState _currentEnemyState;

    public EnemyStateMachine(Enemy enemy, Animator animator, EnemyMovement enemyMovement, EnemyAttack enemyAttack)
    {
        _states.Add(typeof(PatrolState), new PatrolState(enemy, enemyMovement,this));
        _states.Add(typeof(ChaseState), new ChaseState(enemy, enemyMovement, enemyAttack, this));
        _states.Add(typeof(CombatState), new CombatState(enemy, animator, enemyMovement, enemyAttack, this));
    }

    public void Update() =>
        _currentEnemyState.UpdateState();

    public void FixedUpdate() =>
        _currentEnemyState.UpdatePhysicState();

    public void SetState<TState>() where TState : EnemyState
    {
        if (_states.TryGetValue(typeof(TState), out EnemyState nextState))
        {
            _currentEnemyState?.Exit();
            _currentEnemyState = nextState;
            _currentEnemyState?.Enter();
        }
    }
}