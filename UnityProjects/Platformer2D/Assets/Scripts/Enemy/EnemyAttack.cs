using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private AttackDetector _weaponPoint;
    [SerializeField] private float _damage = 2f;

    [field: SerializeField] public float AttackRange { get; private set; }

    private void Attack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(_weaponPoint.transform.position, AttackRange);

        for (int i = 0; i < player.Length; i++)
            if (player[i].TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
                playerHealth.TakeDamage(_damage);
    }

    private void OnDrawGizmosSelected() =>
        Gizmos.DrawWireSphere(_weaponPoint.transform.position, AttackRange);
}