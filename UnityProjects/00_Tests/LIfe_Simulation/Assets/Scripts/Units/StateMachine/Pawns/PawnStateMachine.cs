using System.Collections.Generic;

public class PawnStateMachine : UnitStateMachine
{
    public PawnStateMachine(Worker unit, List<ResourcePoint> resourcePoints)
    {
        States.Add(typeof(IdleState), new IdleState(unit));
        States.Add(typeof(GoldMiningState), new GoldMiningState(unit, resourcePoints[0]));
        States.Add(typeof(TreeMiningState), new TreeMiningState(unit, resourcePoints[1]));
        States.Add(typeof(FoodMiningState), new FoodMiningState(unit, resourcePoints[2]));
    }
}