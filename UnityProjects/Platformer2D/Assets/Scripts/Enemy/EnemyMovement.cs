using System.Collections.Generic;
using UnityEngine;
using static EnemyAnimations;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 3f;

    private Rigidbody2D _rigidbody;
    private bool _isFlipped = true;

    private void Awake() =>    
        _rigidbody = GetComponent<Rigidbody2D>();    

    public void SetTargetToMove(Vector3 direction, float multiplieSpeed = 1f) =>   
        _rigidbody.velocity = (direction - transform.position).normalized * (_speed * multiplieSpeed) * Vector2.right;    

    public void SetDirection(Vector3 direction)
    {
        if (((direction.x - transform.position.x) > 0 && !_isFlipped) || ((direction.x - transform.position.x) < 0 && _isFlipped))
        {
            transform.localScale *= new Vector2(-1f, 1f);
            _isFlipped = !_isFlipped;
        }
    }

    public List<Vector3> GetPointsPosition()
    {
        List<Vector3> points = new();

        for (int i = 0; i < _points.Length; i++)
        {
             points.Add(_points[i].position);
        }

        return points;
    }

    public float GetSpeed() => 
        _rigidbody.velocity.x;
          
    public void ResetSpeed() =>    
        _rigidbody.velocity = Vector3.zero;   
}