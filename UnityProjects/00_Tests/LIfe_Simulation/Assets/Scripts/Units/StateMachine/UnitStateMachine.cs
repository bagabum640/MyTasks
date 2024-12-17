using System;
using System.Collections.Generic;

public class UnitStateMachine
{
    protected readonly Dictionary<Type, UnitState> States = new();

    private UnitState _currentEnemyState;

    public void Update()
    {
        _currentEnemyState.Update();
    }

    public void SetState<TState>() where TState : UnitState
    {
        if (States.TryGetValue(typeof(TState), out UnitState nextState))
        {
            _currentEnemyState?.Exit();
            _currentEnemyState = nextState;
            _currentEnemyState?.Enter();
        }
    }
}