using UnityEngine;
using static EnemyAnimationData;

public class CombatState : EnemyState
{
    private readonly Animator _animator;
    private readonly EnemyAttack _enemyAttack;
    private readonly EnemyMovement _enemyMovement;
    private readonly float _timerBetweenAttack = 1f;

    public CombatState(Enemy enemy, Animator animator, EnemyMovement enemyMovement, EnemyAttack enemyAttack, IStateChanger stateChanger) : base(enemy, stateChanger)
    {
        _animator = animator;
        _enemyAttack = enemyAttack;
        _enemyMovement = enemyMovement;
    }

    public override void UpdateState()
    {
        if (Enemy.IsAggroed && Mathf.Abs(Enemy.GetTargetPosition().x - Enemy.transform.position.x) > _enemyAttack.AttackRange && _enemyMovement.CanMove)
            StateChanger.SetState<ChaseState>();

        if (Enemy.IsAggroed == false)
            StateChanger.SetState<PatrolState>();

        _enemyMovement.GetPathDirection(Enemy.GetTargetPosition());
        
        if (_enemyAttack.AttackDelay >= _timerBetweenAttack)
        {            
            _enemyAttack.ResetTimerAttack();
            _enemyMovement.ProhibitMove();
            _animator.SetTrigger(Attack);         
        }        
    }
}