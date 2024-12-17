using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAnimationData;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _stopMoveDelay;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private WaitForSeconds _stopMoveTime;
    private bool _isFlipped = true;
   
    public bool CanMove { get;private set; } = true;
    public float GetCurrentSpeed => _rigidbody.velocity.x;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _stopMoveTime = new(_stopMoveDelay);
    }

    public void GetPathToMove(Vector3 path, float multiplieSpeed = 1f)
    {
        _animator.SetFloat(MovementSpeed, Mathf.Abs(GetCurrentSpeed));

        _rigidbody.velocity = (path - transform.position).normalized * (_speed * multiplieSpeed) * Vector2.right;
    }

    public void GetPathDirection(Vector3 path)
    {
        float direction = path.x - transform.position.x;

        if ((direction > 0 && !_isFlipped) || (direction < 0 && _isFlipped))
        {
            transform.localScale *= new Vector2(-1f, 1f);
            _isFlipped = !_isFlipped;
        }
    }

    public List<Vector3> GetPointsPosition()
    {
        List<Vector3> points = new();

        for (int i = 0; i < _points.Length; i++)
            points.Add(_points[i].position);

        return points;
    }

    public void ResetSpeed() =>
        _rigidbody.velocity = Vector3.zero;

    public void ProhibitMove() =>
        StartCoroutine(StopMove());

    private IEnumerator StopMove()
    {
        CanMove = false;

        yield return _stopMoveTime;

        CanMove = true;
    }
}