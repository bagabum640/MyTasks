using System.Collections.Generic;
using UnityEngine;

public class Worker : Unit
{
    [SerializeField] private List<ResourcePoint> _resourcePoints;
    [SerializeField] private TownHall _townHall;
    [SerializeField] private Transform _handPosition;

    private PawnStateMachine _stateMachine;
    private Resource _resourse;
    private ResourceType _resourceType;

    public int GoldCost { get; private set; } = 3;
    public int WoodCost { get; private set; } = 3;
    public int FoodCost { get; private set; } = 3;

    public bool ResourseInHand { get; private set; } = false;
    public Vector3 HandPosition => _handPosition.position;

    private new void Awake()
    {
        base.Awake();

        _stateMachine = new PawnStateMachine(this, _resourcePoints);

        SelectRandomResourcePoint();
    }

    private void Update()
    {
        switch (_resourceType)
        {
            case ResourceType.Gold:
                _stateMachine.SetState<GoldMiningState>();
                break;
            case ResourceType.Wood:
                _stateMachine.SetState<TreeMiningState>();
                break;
            case ResourceType.Food:
                _stateMachine.SetState<FoodMiningState>();              
                break;                
        }

        _stateMachine.Update();
    }

    public ResourceType SelectRandomResourcePoint()
    {
        return _resourceType = (ResourceType)(Random.Range(0, _resourcePoints.Count));  
    }

    public void MoveToResoursePoint(ResourcePoint resourcePoint)
    {
        if (resourcePoint != null)
        {
            if ((transform.position - resourcePoint.transform.position).magnitude < 0.2f)
            {
                if (ResourseInHand == false)
                {
                    _resourse = resourcePoint.GetResource(HandPosition);

                    ResourseInHand = true;
                }
            }

            GetPathToMove(resourcePoint.transform.position);
        }
    }

    public void MoveToTownHall()
    {
        if (ResourseInHand)
        {
            if ((transform.position - _townHall.transform.position).magnitude < 0.2f)
            {
                _townHall.GetResource(_resourse);

                ResourseInHand = false;
                SelectRandomResourcePoint();
            }

            _resourse.transform.position = _handPosition.position;
            GetPathToMove(_townHall.transform.position);

            Animator.SetBool("InHand", ResourseInHand);
        }
        else
        {
            Animator.SetBool("InHand", ResourseInHand);           
        }
    }
}