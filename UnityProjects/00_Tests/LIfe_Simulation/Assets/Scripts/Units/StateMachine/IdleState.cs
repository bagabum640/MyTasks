public class IdleState : UnitState
{
    public IdleState(Unit unit):base(unit)
    {
    }

    public override void Enter()
    {
        Unit.ResetSpeed();
    }
}