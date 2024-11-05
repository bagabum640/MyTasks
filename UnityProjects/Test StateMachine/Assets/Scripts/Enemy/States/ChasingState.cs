using UnityEngine;

public class ChasingState : EnemyState
{
    private readonly Transform _target;
    private readonly float _attackRange;

    public ChasingState(Enemy enemy, EnemyStateMachine enemyStateMachine, Transform target, float attackRange) : base(enemy, enemyStateMachine)
    {
        _target = target;
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

        enemy.SetDirection(_target.position);
    }
}
