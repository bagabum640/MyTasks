using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _target;
    [SerializeField] private float _attackRange = 2f;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private readonly List<Vector3> _path = new();
    private readonly int _currentIndex = 0;

    public EnemyStateMachine StateMachine { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public ChasingState ChaseState { get; private set; }
    public CombatState CombatState { get; private set; }
    public bool IsAggroed { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        StateMachine = new EnemyStateMachine();

        PatrolState = new PatrolState(this, StateMachine, _points);
        ChaseState = new ChasingState(this, StateMachine, _target,_attackRange);
        CombatState = new CombatState(this, StateMachine, _target, _animator, _attackRange);
    }

    private void Start()
    {
        PathInit();
        StateMachine.Initialize(PatrolState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.UpdateState();
    }

    public void SetDirection(Vector3 direction, float speed = 3f)
    {
        if (direction.x > transform.position.x)
            _spriteRenderer.flipX = false;
        else if (direction.x < transform.position.x)
            _spriteRenderer.flipX = true;

        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
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