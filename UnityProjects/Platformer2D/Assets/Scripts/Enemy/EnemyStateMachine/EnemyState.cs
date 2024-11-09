public class EnemyState
{
    protected Enemy Enemy;
    protected EnemyStateMachine EnemyStateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        Enemy = enemy;
        EnemyStateMachine = enemyStateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void UpdateState() { }
    public virtual void PhysicUpdateState() { }  
}