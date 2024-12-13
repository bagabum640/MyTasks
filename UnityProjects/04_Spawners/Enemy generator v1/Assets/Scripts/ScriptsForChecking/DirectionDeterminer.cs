using UnityEngine;

public class DirectionDeterminer : MonoBehaviour
{
    [SerializeField] private Directions[] _directions;
        
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
                    return Vector2.up;
                case Directions.down:
                    return Vector2.down;
                case Directions.left:
                    return Vector2.left;
                case Directions.right:
                    return Vector2.right;
            }
        }
                
        return Vector2.zero;
    }
}
