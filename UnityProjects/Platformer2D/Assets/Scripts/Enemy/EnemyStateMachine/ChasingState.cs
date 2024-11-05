using UnityEngine;

public class ChasingState : EnemyState
{
    private readonly Transform _player;

    public ChasingState(Enemy enemy, EnemyStateMachine enemyStateMachine, Transform player) : base(enemy, enemyStateMachine)
    {
        _player = player;
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
        {
            enemy.stateMachine.ChangeState(enemy.PatrolState);
        }

        enemy.SetDirection(_player.position);
    }
}