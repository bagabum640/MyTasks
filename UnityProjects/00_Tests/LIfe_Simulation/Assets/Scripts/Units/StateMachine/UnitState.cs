public class UnitState
{
    protected Unit Unit;

    public UnitState(Unit unit)
    {
        Unit = unit;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}