using UnityEngine;

public class HealPoint : MonoBehaviour
{
    [SerializeField] private float _healthRestoreCount = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            if (!playerHealth.CanTake())
            {
                playerHealth.RestoreHealth(_healthRestoreCount);
                Destroy(gameObject);
            }
        }
    }
}