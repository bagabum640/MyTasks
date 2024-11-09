using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    private readonly Dictionary<Type, EnemyState> _states = new();

    public EnemyState CurrentEnemyState { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public CombatState CombatState { get; private set; }

    public EnemyStateMachine(Enemy enemy, Animator animator, EnemyMovement enemyMovement, EnemyAttack enemyAttack)
    {
        _states.Add(typeof(PatrolState), new PatrolState(enemy, this, enemyMovement));
        _states.Add(typeof(ChaseState), new ChaseState(enemy, this, enemyMovement, enemyAttack));
        _states.Add(typeof(CombatState), new CombatState(enemy, this, animator, enemyAttack));
    }

    public void SetState<Type>() where Type : EnemyState
    {
        if (_states.TryGetValue(typeof(Type), out EnemyState nextState))
        {
            CurrentEnemyState?.Exit();
            CurrentEnemyState = nextState;
            CurrentEnemyState?.Enter();
        }
    }
}