using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;

    private readonly List<Vector2> _path = new();

    private SpriteRenderer _spriteRenderer;
    private Vector3 _currentPath;
    private int _currentIndex = 0;

    private void Awake() =>
       _spriteRenderer = GetComponent<SpriteRenderer>();

    private void Start()
    {
        if (_points.Length > 0)
        {
            foreach (Transform point in _points)
            {
                _path.Add(point.position);
                point.gameObject.SetActive(false);
            }

            _currentPath = _path[_currentIndex];
        }
        else
        {
            _currentPath = transform.position;
        }

        Flip();
    }

    private void FixedUpdate() =>
        MoveToPoint();

    private void MoveToPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentPath, _speed * Time.deltaTime);

        if (transform.position == _currentPath)
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        if (_currentIndex >= _path.Count)
            _currentIndex = 0;
        else
            _currentPath = _path[_currentIndex++];

        Flip();
    }

    private void Flip()
    {
        if (_currentPath.x > transform.position.x)
            _spriteRenderer.flipX = false;
        else if (_currentPath.x < transform.position.x)
            _spriteRenderer.flipX = true;
    }
}