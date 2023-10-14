using UnityEngine;

public class DirectionDeterminer : MonoBehaviour
{
    [SerializeField] private Directions[] _directions;

    private Vector2 _up = new (0, 1);
    private Vector2 _down = new(0, -1);
    private Vector2 _left = new(-1, 0);
    private Vector2 _right = new(1, 0);
    private Vector2 _stop = new(0, 0);

    private enum Directions
    {
        up,
        down,
        left,
        right,
    }

    public Vector2 GetDirection()
    {
        if (_directions.Length > 0)
        {
            Directions direction = _directions[Random.Range(0, _directions.Length)];
            
            switch (direction)
            {
                case Directions.up:
                    return _up;
                case Directions.down:
                    return _down;
                case Directions.left:
                    return _left;
                case Directions.right:
                    return _right;
            }
        }
                
        return _stop;
    }
}
