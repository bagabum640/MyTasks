using System.Collections;
using UnityEngine;

public class CombatState : EnemyState
{
    private float _timer;

    private readonly Animator _animator;
    private readonly Transform _target;
    private readonly float _attackRange;
    private readonly float _timerBetweenShots = 2f;

    public CombatState(Enemy enemy, EnemyStateMachine enemyStateMachine, Transform target, Animator animator, float attackRange) : base(enemy, enemyStateMachine)
    {
        _target = target;
        _attackRange = attackRange;
        _animator = animator;
        _timer = _timerBetweenShots;
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
        if ((_target.position - enemy.transform.position).sqrMagnitude > _attackRange)
            enemy.StateMachine.ChangeState(enemy.ChaseState);

        if (_timer >= _timerBetweenShots)
        {
            
            _timer = 0f;
        }

        _timer += Time.deltaTime;
    }

    private IEnumerator Attack()
    {
        WaitForSeconds waitForSeconds = new(1f);

        _animator.SetBool("IsAttacking", true);

        yield return waitForSeconds;

        _animator.SetBool("IsAttacking", false);
    }
}