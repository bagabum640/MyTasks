using UnityEngine;

public class ChaseState : EnemyState
{
    private readonly EnemyMovement _enemyMovement;
    private readonly EnemyAttack _enemyAttack;
    private readonly int _multiplieSpeed = 2;

    public ChaseState(Enemy enemy, EnemyMovement enemyMovement, EnemyAttack enemyAttack, IStateChanger stateChanger) : base(enemy, stateChanger)
    {
        _enemyAttack = enemyAttack;
        _enemyMovement = enemyMovement;
    }

    public override void Enter() =>
        _enemyMovement.GetPathDirection(Enemy.GetTargetPosition());

    public override void Exit() =>
        _enemyMovement.ResetSpeed();

    public override void UpdatePhysicState()
    {
        if (Enemy.IsAggroed == false)
            StateChanger.SetState<PatrolState>();

        if (Enemy.IsAggroed && Mathf.Abs(Enemy.GetTargetPosition().x - Enemy.transform.position.x) <= _enemyAttack.AttackRange)
            StateChanger.SetState<CombatState>();

        _enemyMovement.GetPathToMove(Enemy.GetTargetPosition(), _multiplieSpeed);
    }
}