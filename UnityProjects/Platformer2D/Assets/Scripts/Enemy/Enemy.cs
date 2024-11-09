using UnityEngine;
using static EnemyAnimations;

[RequireComponent(typeof(EnemyMovement),
                  typeof(EnemyHealth),
                  typeof(Animator))]
[RequireComponent(typeof(EnemyAttack))]
public class Enemy : MonoBehaviour
{
    private Transform _target;
    private Animator _animator;
    private EnemyHealth _enemyHealth;
    private EnemyStateMachine _stateMachine;
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;

    public bool IsAggroed { get; private set; }
   [field: SerializeField] public bool IsFighted { get; private set; }

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();

        _stateMachine = new EnemyStateMachine(this, _animator, _enemyMovement, _enemyAttack);
        _stateMachine.SetState<PatrolState>();
    }

    private void Update()
    {
        if (_enemyHealth.Health > 0)
        {
            _stateMachine.CurrentEnemyState.UpdateState();
            _animator.SetFloat(MovementSpeed, Mathf.Abs(_enemyMovement.GetSpeed()));
        }
    }

    private void FixedUpdate()
    {
        if (_enemyHealth.Health > 0)       
            _stateMachine.CurrentEnemyState.PhysicUpdateState();       
    }

    public Vector3 GetTargetPosition()
    {
        if (_target != null)
        {
            return _target.position;
        }

        return Vector3.zero;
    }

    public void SetTarget(Transform target) =>
        _target = target;

    public void SetAggroStatus(bool isAggroed) =>
        IsAggroed = isAggroed;

    public void SetFightStatus(bool isFighted) =>
        IsFighted = isFighted;
}