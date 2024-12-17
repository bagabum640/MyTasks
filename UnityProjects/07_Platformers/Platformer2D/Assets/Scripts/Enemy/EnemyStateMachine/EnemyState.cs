public class EnemyState
{
    protected Enemy Enemy;
    protected IStateChanger StateChanger;

    public EnemyState(Enemy enemy,IStateChanger stateChanger)
    {
        Enemy = enemy;
        StateChanger = stateChanger;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void UpdateState() { }
    public virtual void UpdatePhysicState() { }  
}