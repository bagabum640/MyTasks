using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackRange = 1f;

    [field: SerializeField] public Bullet Bullet { get; private set; }

    public EnemyStateMachine StateMachine { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public ChasingState ChaseState { get; private set; }
    public CombatState CombatState { get; private set; }
    public bool IsAggroed { get; private set; }

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        PatrolState = new PatrolState(this, StateMachine, _points);
        ChaseState = new ChasingState(this, StateMachine, _target, _attackRange);
        CombatState = new CombatState(this, StateMachine, _target, _attackRange);
    }

    private void Start()
    {
        StateMachine.Initialize(PatrolState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.UpdateState();
    }

    public void SetDirection(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, _speed * Time.deltaTime);
    }

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }
}