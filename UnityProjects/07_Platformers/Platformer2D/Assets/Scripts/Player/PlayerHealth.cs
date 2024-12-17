using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealthAmount = 10;
    [SerializeField] private float _currentHealthAmount;

    private readonly float _minHealthAmount = 0f;

    private PlayerAnimations _playerAnimations;

    public bool IsAlive { get; private set; } = true;

    public event Action PlayerDied;

    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();

        _currentHealthAmount = _maxHealthAmount;
    }

    public void TakeDamage(float damage)
    {
        _currentHealthAmount = Mathf.Clamp(_currentHealthAmount - Mathf.Abs(damage), _minHealthAmount, _maxHealthAmount);

        if (_currentHealthAmount <= 0)
        {
            IsAlive = false;
            PlayerDied?.Invoke();
            _playerAnimations.DeathAnimation();
        }
        else
        {
            _playerAnimations.HurtAnimation();
        }
    }

    public void RestoreHealth(int healthAmount) =>
        _currentHealthAmount = Mathf.Clamp(_currentHealthAmount + Mathf.Abs(healthAmount), _minHealthAmount, _maxHealthAmount);

    public bool GetPossibleOfHealing() =>
        _currentHealthAmount < _maxHealthAmount;
}