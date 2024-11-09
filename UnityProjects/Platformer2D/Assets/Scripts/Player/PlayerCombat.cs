using UnityEngine;
using static PlayerAnimations;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay;

    private float _timer;

    private void Update() =>
        _timer += Time.deltaTime;

    public void AttackDelay()
    {
        if (_timer >= _attackDelay)
        {
            _timer = 0;

            PlayerAnimator.SetTrigger(Attacking);
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D hit in hitEnemies)
            if (hit.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
                enemy.TakeDamage(_damage);
    }

    private void OnDrawGizmosSelected() =>
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
}