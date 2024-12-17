public class TreeMiningState : UnitState
{
    private readonly Worker _pawn;
    private readonly TreeMine _treeMine;

    public TreeMiningState(Worker unit, ResourcePoint treeMine) : base(unit)
    {
        _pawn = unit;
        _treeMine = (TreeMine)treeMine;
    }

    public override void Update()
    {
        _pawn.MoveToResoursePoint(_treeMine);
        _pawn.MoveToTownHall();
    }
}