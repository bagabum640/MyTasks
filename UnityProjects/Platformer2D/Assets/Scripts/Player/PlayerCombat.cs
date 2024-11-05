using UnityEngine;
using static PlayerAnimator;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackDetecting();
            _animator.SetTrigger(Attacking);
        }
    }

    private void AttackDetecting()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D hit in hitEnemies)       
            if (hit.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
                enemy.TakeDamage(_damage);       
    }
}