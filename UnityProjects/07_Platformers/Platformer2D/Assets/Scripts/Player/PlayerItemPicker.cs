using UnityEngine;

public class PlayerItemPicker : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICollectable>(out ICollectable item))
        {
            if (item is HealthKit healthKit)
            {
                if (_playerHealth.GetPossibleOfHealing())
                {
                    _playerHealth.RestoreHealth(healthKit.HealthAmount);
                    Destroy(healthKit.gameObject);
                }
            }

            item.Collect(); 
        }
    }
}