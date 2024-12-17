using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private float _damage = 2f;

    [field: SerializeField] public float AttackRange { get; private set; }
    public float AttackDelay { get; private set; }

    private void Update() =>
        AttackDelay += Time.deltaTime;

    public void ResetTimerAttack()
    {
        AttackDelay = 0;
    }
    
    private void Attack()  //Âûחûגאועסÿ קונוח Event ג Animation
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(_weaponPoint.transform.position, AttackRange);

        for (int i = 0; i < player.Length; i++)
            if (player[i].TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
                playerHealth.TakeDamage(_damage);       
    }

    private void OnDrawGizmosSelected() =>
        Gizmos.DrawWireSphere(_weaponPoint.transform.position, AttackRange);
}