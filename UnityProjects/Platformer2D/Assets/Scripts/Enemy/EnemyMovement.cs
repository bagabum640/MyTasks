using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),
                  typeof(EnemyHealth))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;

    private SpriteRenderer _spriteRenderer;
    private EnemyHealth _enemyHealth;
    private int _currentIndex = 0;

    private readonly List<Vector3> _path = new();
    private readonly float _distanceToTarget = 0.2f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyHealth = GetComponent<EnemyHealth>();

        Init();
    }

    private void Update() =>
        MoveToTarget(_path[_currentIndex]);

    private void Init()
    {
        if (_points.Length > 0)
        {
            foreach (Transform point in _points)
            {
                _path.Add(point.position);
                point.gameObject.SetActive(false);
            }

            _path[_currentIndex] = _points[_currentIndex].position;
        }
        else
        {
            _path[_currentIndex] = transform.position;
        }
    }

    public void MoveToTarget(Vector3 direction)
    {
        if (_enemyHealth.Health > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction, _speed * Time.deltaTime);

            if ((transform.position - direction).sqrMagnitude < _distanceToTarget)
                _currentIndex = ++_currentIndex % _path.Count;
        }
        else
        {
            _speed = 0;
        }

        Flip(direction);
    }

    private void Flip(Vector3 target)
    {
        if (target.x > transform.position.x)
            _spriteRenderer.flipX = false;
        else if (target.x < transform.position.x)
            _spriteRenderer.flipX = true;
    }
}