using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private EnemyHealth _enemyHealth;
    private EnemyMovement _movement;
    private SpriteRenderer _spriteRenderer;
    private int _currentIndex = 0;
    private float _distanceToTarget = 0.2f;

    public EnemyStateMachine stateMachine;


    private readonly List<Vector3> _path = new();

    public PatrolState PatrolState { get; set; }
    public ChasingState ChaseState { get; set; }
    public bool IsAggroed { get; set; }

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponent<EnemyMovement>();

        stateMachine = new EnemyStateMachine();

        PatrolState = new PatrolState(this, stateMachine, _points);
        ChaseState = new ChasingState(this, stateMachine, _target);
    }

    private void Start()
    {
        PathInit();
        stateMachine.Initialize(PatrolState);
    }

    private void Update()
    {
        stateMachine.CurrentEnemyState.UpdateState();
    }

    public void SetDirection(Vector3 direction)
    {
        if (_target.position.x > transform.position.x)
            _spriteRenderer.flipX = false;
        else if (_target.position.x < transform.position.x)
            _spriteRenderer.flipX = true;

       
    }

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    private void PathInit()
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
}