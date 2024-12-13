using UnityEngine;
using static EnemyAnimationData;

[RequireComponent(typeof(Animator),
                  typeof(Rigidbody2D))]
public class EnemyDamageHandler : MonoBehaviour
{
    private readonly int _minHealth = 0;

    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth = 10;

    private Animator _animator;

    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - Mathf.Abs(damage), _minHealth, _maxHealth);

        if (_currentHealth <= 0)
            Die();
        else
            _animator.SetTrigger(Hurt);
    }

    private void Die()
    {
        IsAlive = false;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        _animator.SetTrigger(Death);
    }
}