using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay;

    private PlayerAnimations _playerAnimations;

    private float _timer;

    private void Awake() =>   
        _playerAnimations = GetComponent<PlayerAnimations>();    

    private void Update() =>
        _timer += Time.deltaTime;

    public void AttackDelay()
    {
        if (_timer >= _attackDelay)
        {
            _timer = 0;

            _playerAnimations.AttackAnimation();
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