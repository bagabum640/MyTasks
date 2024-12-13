using UnityEngine;
using static EnemyAnimationData;

[RequireComponent(typeof(EnemyMovement),
                  typeof(EnemyDamageHandler),
                  typeof(Animator))]
[RequireComponent(typeof(EnemyAttack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private AggroDetector _aggroDetector;

    private Transform _target;
    private Animator _animator;
    private EnemyDamageHandler _enemyDamageHandler;
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;

    public EnemyStateMachine StateMachine { get; private set; }
    
    public bool IsAggroed { get; private set; }
    public bool IsFighted { get; private set; }

    private void Awake()
    {
        _enemyDamageHandler = GetComponent<EnemyDamageHandler>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();

        StateMachine = new EnemyStateMachine(this, _animator, _enemyMovement, _enemyAttack);
        StateMachine.SetState<PatrolState>();
    }

    private void OnEnable()
    {
        _aggroDetector.IsSetTarget += SetTarget;
        _aggroDetector.IsLostTarget += LossOfTarget;
    }

    private void OnDisable()
    {
        _aggroDetector.IsSetTarget -= SetTarget;
        _aggroDetector.IsLostTarget -= LossOfTarget;
    }

    private void Update()
    {
        if (_enemyDamageHandler.IsAlive)
        {
            StateMachine.Update();
            _animator.SetFloat(MovementSpeed, Mathf.Abs(_enemyMovement.GetCurrentSpeed));
        }
    }

    private void FixedUpdate()
    {
        if (_enemyDamageHandler.IsAlive)
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

    public void SetTarget(Transform target)
    {
        _target = target;
        IsAggroed = true;
    }

    private void LossOfTarget()
    {
        _target = null;
        IsAggroed = false;
    }     
}