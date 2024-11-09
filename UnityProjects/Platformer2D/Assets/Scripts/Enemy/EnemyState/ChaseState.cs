public class ChaseState : EnemyState
{
    private readonly EnemyMovement _enemyMovement;
    private readonly EnemyAttack _enemyAttack;
    private readonly float _multiplieSpeed = 2f;

    public ChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine, EnemyMovement enemyMovement, EnemyAttack enemyAttack) : base(enemy, enemyStateMachine)
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
        if (!Enemy.IsAggroed)
            EnemyStateMachine.SetState<PatrolState>();

        if (Enemy.IsFighted && (Enemy.GetTargetPosition() - Enemy.transform.position).sqrMagnitude <= _enemyAttack.AttackRange)
            EnemyStateMachine.SetState<CombatState>();

        _enemyMovement.SetTargetToMove(Enemy.GetTargetPosition(), _multiplieSpeed);
    }
}