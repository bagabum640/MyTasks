using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    [SerializeField] Transform _leftMarker;
    [SerializeField] Transform _rightMarker;

    private Rigidbody2D _rigidBody2D;
    private Vector2 _direction;
    public bool _isMovementPossible = true;
    private float _leftBorder;
    private float _rightBorder;
    private float _pathTime;
    private float _currentTargetPosition;

    private void Start()
    {
        _leftBorder = _leftMarker.position.x;
        _rightBorder = _rightMarker.position.x;
        _rigidBody2D = GetComponent<Rigidbody2D>();

        DetermineDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void AllowMovement() => _isMovementPossible = true;

    public void ProhibitMovement() => _isMovementPossible = false;

    private void Move()
    {
        if (_pathTime > 0)
        {
            if (_isMovementPossible)
            {
                _rigidBody2D.velocity = _speed * _direction;
                _pathTime -= Time.fixedDeltaTime;
            }
            else
            {
                _rigidBody2D.velocity = Vector2.zero;
            }
        }
        else
        {
            DetermineDirection();
        }
    }

    private void DetermineDirection()
    {
        Vector3 leftTurn = new(0, 180, 0);

        if (_currentTargetPosition == _leftBorder)
        {
            _currentTargetPosition = _rightBorder;
            _direction = Vector2.right;
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            _currentTargetPosition = _leftBorder;
            _direction = Vector2.left;
            transform.eulerAngles = leftTurn;
        }

        _pathTime = Mathf.Abs((_currentTargetPosition - transform.position.x) / _speed);
    }
}
