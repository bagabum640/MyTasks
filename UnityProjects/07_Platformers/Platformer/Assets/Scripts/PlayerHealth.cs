using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;

    private readonly int _maxHealtPoint = 3;

    private Animator _animator;    
    private int _currentHealthPoint;

    private void Start()
    {
        _currentHealthPoint = _maxHealtPoint;
        _animator = GetComponent<Animator>();
        _healthBar.AddHeart(_currentHealthPoint);
    }

    public void TakeDamage(int damage = 1)
    {
        _currentHealthPoint -= damage;
        _healthBar.RemoveHeart(damage);
        _animator.SetTrigger(PlayerAnimator.Params.Hurt);

        if (_currentHealthPoint <= 0)
            TryCollectHeart(3);
    }
       
    public bool TryCollectHeart(int healPoint = 1)
    {
        if (_currentHealthPoint < _maxHealtPoint)
        {
            if (_currentHealthPoint + healPoint <= _maxHealtPoint)
            {
                _currentHealthPoint += healPoint;
            }                
            else
            {
                healPoint = _maxHealtPoint - _currentHealthPoint;
                _currentHealthPoint = _maxHealtPoint;                
            }
               
            _healthBar.AddHeart(healPoint);
            return true;
        }

        return false;
    }
}
