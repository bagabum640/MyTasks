using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private float _currentHealth;

    private PlayerAnimations _playerAnimations;

    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();

        _currentHealth = _maxHealth;

        HealthKit.IsHealing += RestoreHealth;
    }   

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            PlayerDead();
        else
            _playerAnimations.HurtAnimation();
    }

    public void RestoreHealth(float heal)
    {
        if (_currentHealth != _maxHealth)
        {
            _currentHealth += heal;

            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;            
        }
    }

    private void PlayerDead()
    {
        IsAlive = false;

        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Player>().enabled = false;

        _playerAnimations.DeathAnimation();
    }
}