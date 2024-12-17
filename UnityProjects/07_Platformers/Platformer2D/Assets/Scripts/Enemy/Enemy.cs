using UnityEngine;
using static EnemyAnimationData;

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
    private EnemyStateMachine _stateMachine;
    
    public bool IsAggroed { get; private set; }

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();

        _stateMachine = new EnemyStateMachine(this, _animator, _enemyMovement, _enemyAttack);
        _stateMachine.SetState<PatrolState>();
    }

    private void OnEnable()
    {
        _aggroDetector.TargetFound += SetTarget;
        _aggroDetector.TargetLost += LossOfTarget;
    }

    private void OnDisable()
    {
        _aggroDetector.TargetFound -= SetTarget;
        _aggroDetector.TargetLost -= LossOfTarget;
    }

    private void Update()
    {
        if (_enemyHealth.IsAlive)       
            _stateMachine.Update();                   
    }

    private void FixedUpdate()
    {
        if (_enemyHealth.IsAlive)
            _stateMachine.FixedUpdate();
    }

    public Vector3 GetTargetPosition()
    {
        if (_target != null)       
            return _target.position;
        
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