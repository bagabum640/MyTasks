using UnityEngine;
using static PlayerAnimations;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private float _currentHealth;

    public bool IsAlive { get; private set; } = true;

    private void Awake() =>
        _currentHealth = _maxHealth;
    
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            PlayerDead();
        }
        else
        {
            PlayerAnimator.SetTrigger(Hurt);
        }
    }

    public void RestoreHealth(float health)
    {
        _currentHealth += health;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    public bool CanTake()
    {
        return _currentHealth == _maxHealth;
    }

    private void PlayerDead()
    {
        IsAlive = false;

        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Player>().enabled = false;

        PlayerAnimator.SetTrigger(Death);
    }
}