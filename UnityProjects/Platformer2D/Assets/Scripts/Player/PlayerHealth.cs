using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerHealth : MonoBehaviour
{
    private readonly float _minHealth = 0f;
    private readonly float _maxHealth = 10;

    [SerializeField] private float _currentHealth;

    private PlayerAnimations _playerAnimations;

    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - Mathf.Abs(damage), _minHealth, _maxHealth);

        if (_currentHealth <= 0)
            PlayerDead();
        else
            _playerAnimations.HurtAnimation();
    }

    public void RestoreHealth(HealthKit healthKit) =>   
        _currentHealth = Mathf.Clamp(_currentHealth + Mathf.Abs(healthKit.HealthAmount), _minHealth, _maxHealth);
    
    public bool GetPossibleOfHealing() =>
        _currentHealth < _maxHealth;

    private void PlayerDead()
    {
        IsAlive = false;

        SetChildrenActiveState();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Player>().enabled = false;

        _playerAnimations.DeathAnimation();
    }

    private void SetChildrenActiveState()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
}