using System;
using UnityEngine;
using static EnemyAnimationData;

[RequireComponent(typeof(Animator),
                  typeof(Rigidbody2D))]
public class EnemyHealth : MonoBehaviour
{
    private readonly int _minHealth = 0;

    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth = 10;

    private Animator _animator;

    public bool IsAlive { get; private set; } = true;

    public event Action EnemyDied;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - Mathf.Abs(damage), _minHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            IsAlive = false;
            EnemyDied?.Invoke();
            _animator.SetTrigger(Death);
        }         
        else
            _animator.SetTrigger(Hurt);
    }
}