public class EnemyState
{
    protected Enemy Enemy;

    public EnemyState(Enemy enemy)
    {
        Enemy = enemy;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void UpdateState() { }
    public virtual void PhysicUpdateState() { }  
}