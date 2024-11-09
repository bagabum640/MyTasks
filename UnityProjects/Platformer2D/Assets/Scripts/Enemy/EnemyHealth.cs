using UnityEngine;
using static EnemyAnimations;

[RequireComponent(typeof(Animator),
                  typeof(Rigidbody2D))]
public class EnemyHealth : MonoBehaviour
{
    [field: SerializeField] public int Health { get; private set; }

    private Animator _animator;

    private void Awake() =>    
        _animator = GetComponent<Animator>();   

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
        else
        {
            _animator.SetTrigger(Hurt);
        }
    }

    private void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        _animator.SetTrigger(Death);      
    }
}