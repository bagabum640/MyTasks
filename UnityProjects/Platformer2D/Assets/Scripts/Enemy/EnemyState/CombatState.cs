using UnityEngine;
using static EnemyAnimations;

public class CombatState : EnemyState
{
    private readonly Animator _animator;
    private readonly EnemyAttack _enemyAttack;
    private readonly float _timerBetweenAttack = 1f;

    private float _timer;

    public CombatState(Enemy enemy, EnemyStateMachine enemyStateMachine, Animator animator, EnemyAttack enemyAttack) : base(enemy, enemyStateMachine)
    {
        _animator = animator;
        _enemyAttack = enemyAttack;
        _timer = _timerBetweenAttack;
    }

    public override void UpdateState()
    {
        if (Enemy.IsAggroed && (Enemy.GetTargetPosition() - Enemy.transform.position).sqrMagnitude > _enemyAttack.AttackRange)
            EnemyStateMachine.SetState<ChaseState>();

        if (!Enemy.IsAggroed)
            EnemyStateMachine.SetState<PatrolState>();

        if (Enemy.IsFighted)
        {
            if (_timer >= _timerBetweenAttack)
            {
                _timer = 0f;

                _animator.SetTrigger(Attack);
            }
        }
        else
        {
            EnemyStateMachine.SetState<ChaseState>();
        }

        _timer += Time.deltaTime;
    }
}