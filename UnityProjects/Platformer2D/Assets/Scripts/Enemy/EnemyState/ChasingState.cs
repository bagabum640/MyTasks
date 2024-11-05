using UnityEngine;

public class ChasingState : EnemyState
{
    private readonly Transform _target;
    private readonly float _attackRange;
    private readonly float _speed = 5f;

    public ChasingState(Enemy enemy, EnemyStateMachine enemyStateMachine, Transform player,float attackRange) : base(enemy, enemyStateMachine)
    {
        _target = player;
        _attackRange = attackRange;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (!enemy.IsAggroed)       
            enemy.StateMachine.ChangeState(enemy.PatrolState);
        
        if (enemy.IsAggroed && (enemy.transform.position - _target.position).sqrMagnitude <= _attackRange)
            enemyStateMachine.ChangeState(enemy.CombatState);

        enemy.SetDirection(_target.position, _speed);
    }
}