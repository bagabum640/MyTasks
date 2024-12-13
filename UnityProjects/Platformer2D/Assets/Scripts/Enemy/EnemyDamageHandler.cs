using UnityEngine;
using static EnemyAnimationData;

[RequireComponent(typeof(Animator),
                  typeof(Rigidbody2D))]
public class EnemyDamageHandler : MonoBehaviour
{
    [field: SerializeField] public int Health { get; private set; }

    private Animator _animator;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    public void TakeDamage(int damage)
    {
        Health -= Mathf.Abs(damage);

        if (Health <= 0)
            Die();
        else
            _animator.SetTrigger(Hurt);
    }

    private void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        _animator.SetTrigger(Death);
    }
}