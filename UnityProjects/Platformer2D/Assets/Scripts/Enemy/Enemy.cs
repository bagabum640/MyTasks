using UnityEngine;
using static EnemyAnimations;

[RequireComponent(typeof(EnemyMovement),
                  typeof(EnemyHealth),
                  typeof(Animator))]
[RequireComponent(typeof(EnemyAttack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private AggroDetector _aggroDetector;

    private Transform _target;
    private Animator _animator;
    private EnemyHealth _enemyHealth;
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;

    public EnemyStateMachine StateMachine { get; private set; }
    
    public bool IsAggroed { get; private set; }
    public bool IsFighted { get; private set; }

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();

        StateMachine = new EnemyStateMachine(this, _animator, _enemyMovement, _enemyAttack);
        StateMachine.SetState<PatrolState>();
    }

    private void OnEnable()
    {
        _aggroDetector.IsAggroed += SetActiveAggroStatus;
        _aggroDetector.IsExitedAggro += SetDeactiveAggroStatus;
        _aggroDetector.IsSetTarget += SetTarget;
        _aggroDetector.IsLostTarget += LossOfTarget;
    }

    private void OnDisable()
    {
        _aggroDetector.IsAggroed -= SetActiveAggroStatus;
        _aggroDetector.IsAggroed -= SetDeactiveAggroStatus;
        _aggroDetector.IsSetTarget -= SetTarget;
        _aggroDetector.IsLostTarget -= LossOfTarget;
    }

    private void Update()
    {
        if (_enemyHealth.Health > 0)
        {
            StateMachine.Update();
            _animator.SetFloat(MovementSpeed, Mathf.Abs(_enemyMovement.GetSpeed()));
        }
    }

    private void FixedUpdate()
    {
        if (_enemyHealth.Health > 0)
            StateMachine.FixedUpdate();
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

    private void LossOfTarget() =>
        _target = null;

    private void SetActiveAggroStatus() =>   
        IsAggroed = true;  

    private void SetDeactiveAggroStatus() =>   
        IsAggroed = false;   
}