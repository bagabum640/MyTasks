using UnityEngine;

public class CombatState : EnemyState
{
    private float _timer;

    private readonly Transform _target;
    private readonly float _attackRange;
    private readonly float _timerBetweenShots = 2f;

    public CombatState(Enemy enemy, EnemyStateMachine enemyStateMachine,Transform target, float attackRange) : base(enemy, enemyStateMachine)
    {
        _target = target;
        _attackRange = attackRange;
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
        if((_target.position - enemy.transform.position).sqrMagnitude > _attackRange)
            enemy.StateMachine.ChangeState(enemy.ChaseState);

        if (_timer >= _timerBetweenShots)
        {
            _timer = 0f;

            Bullet bullet = Bullet.Instantiate(enemy.Bullet, enemy.transform.position, Quaternion.identity);
            bullet.SetDirection((_target.position - enemy.transform.position).normalized);
        }

        _timer += Time.deltaTime;
        Debug.Log("Ïèó-ïèó");
    }
}