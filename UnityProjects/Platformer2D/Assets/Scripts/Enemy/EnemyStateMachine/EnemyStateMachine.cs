public class EnemyStateMachine
{
    public EnemyState CurrentEnemyState {  get; set; }

    public void Initialize(EnemyState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(EnemyState nextState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = nextState;
        CurrentEnemyState.EnterState();
    }
}