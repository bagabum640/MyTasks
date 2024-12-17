public class FoodMiningState : UnitState
{
    private readonly Worker _pawn;
    private readonly FoodMine _foodMine;

    public FoodMiningState(Worker unit, ResourcePoint foodMine) : base(unit)
    {
        _pawn = unit;
        _foodMine = (FoodMine)foodMine;
    }

    public override void Update()
    {
        _pawn.MoveToResoursePoint(_foodMine);
        _pawn.MoveToTownHall();
    }
}