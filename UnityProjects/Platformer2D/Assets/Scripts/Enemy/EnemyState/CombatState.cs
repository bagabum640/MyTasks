using UnityEngine;
using static EnemyAnimations;

public class CombatState : EnemyState
{
    private readonly Animator _animator;
    private readonly EnemyAttack _enemyAttack;
    private readonly EnemyMovement _enemyMovement;
    private readonly float _timerBetweenAttack = 1f;

    public CombatState(Enemy enemy, Animator animator, EnemyMovement enemyMovement,EnemyAttack enemyAttack) : base(enemy)
    {
        _animator = animator;
        _enemyAttack = enemyAttack;
        _enemyMovement = enemyMovement;
    }

    public override void UpdateState()
    {
        if (Enemy.IsAggroed && Mathf.Abs(Enemy.GetTargetPosition().x - Enemy.transform.position.x) > _enemyAttack.AttackRange)
            Enemy.StateMachine.SetState<ChaseState>();

        if (Enemy.IsAggroed == false)
            Enemy.StateMachine.SetState<PatrolState>();

        _enemyMovement.SetDirection(Enemy.GetTargetPosition());

        if (_enemyAttack.AttackDelay >= _timerBetweenAttack)
        {
            _enemyAttack.ResetTimerAttack();

            _animator.SetTrigger(Attack);
        }
    }
}