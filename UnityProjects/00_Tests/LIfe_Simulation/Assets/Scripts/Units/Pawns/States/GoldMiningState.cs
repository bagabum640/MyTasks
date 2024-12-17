public class GoldMiningState : UnitState
{
    private readonly Worker _pawn;
    private readonly GoldMine _goldMine;

    public GoldMiningState(Worker unit, ResourcePoint goldMine) : base(unit)
    {
        _pawn = unit;
        _goldMine = (GoldMine)goldMine;
    }

    public override void Update()
    {
        _pawn.MoveToResoursePoint(_goldMine);
        _pawn.MoveToTownHall();
    }
}