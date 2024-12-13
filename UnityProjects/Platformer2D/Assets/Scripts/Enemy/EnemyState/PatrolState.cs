using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private readonly EnemyMovement _enemyMovement;
    private readonly List<Vector3> _path = new();
    private readonly float _distanceToPoint = 1f;

    private int _pointNumber;

    public PatrolState(Enemy enemy, EnemyMovement enemyMovement) : base(enemy)
    {
        _enemyMovement = enemyMovement;

        PathInit(_enemyMovement.GetPointsPosition());
    }

    public override void Enter() =>
        _enemyMovement.GetPathDirection(_path[_pointNumber]);

    public override void Exit() =>
        _enemyMovement.ResetSpeed();

    public override void PhysicUpdateState()
    {
        if (Enemy.IsAggroed)        
            Enemy.StateMachine.SetState<ChaseState>();
        
        if (Mathf.Abs(_path[_pointNumber].x - Enemy.transform.position.x) <= _distanceToPoint)
        {
            _pointNumber = ++_pointNumber % _path.Count;
            _enemyMovement.GetPathDirection(_path[_pointNumber]);
        }

        _enemyMovement.GetPathToMove(_path[_pointNumber]);
    }

    private void PathInit(List<Vector3> points)
    {
        if (points.Count > 0)
        {
            foreach (Vector3 point in points)
                _path.Add(point);

            _path[_pointNumber] = points[_pointNumber];
        }
        else
        {
            _path[_pointNumber] = Enemy.transform.position;
        }
    }
}