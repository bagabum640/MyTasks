using UnityEngine;

public class ChaseState : EnemyState
{
    private readonly EnemyMovement _enemyMovement;
    private readonly EnemyAttack _enemyAttack;
    private readonly float _multiplieSpeed = 2f;

    public ChaseState(Enemy enemy, EnemyMovement enemyMovement, EnemyAttack enemyAttack) : base(enemy)
    {
        _enemyAttack = enemyAttack;
        _enemyMovement = enemyMovement;
    }

    public override void Enter() =>
        _enemyMovement.SetDirection(Enemy.GetTargetPosition());

    public override void Exit() =>
        _enemyMovement.ResetSpeed();

    public override void PhysicUpdateState()
    {
        if (Enemy.IsAggroed == false)
            Enemy.StateMachine.SetState<PatrolState>();

        if (Enemy.IsAggroed && Mathf.Abs(Enemy.GetTargetPosition().x - Enemy.transform.position.x) <= _enemyAttack.AttackRange)
            Enemy.StateMachine.SetState<CombatState>();

        _enemyMovement.SetTargetToMove(Enemy.GetTargetPosition(), _multiplieSpeed);
    }
}