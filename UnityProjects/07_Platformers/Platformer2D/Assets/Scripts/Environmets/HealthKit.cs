using UnityEngine;

public class HealthKit : MonoBehaviour,ICollectable
{
    [field: SerializeField] public float HealthAmount { get; private set; } = 3;

    public void Collect()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth.GetPossibleOfHealing())
        {
            playerHealth.RestoreHealth(this);
            Destroy(gameObject);
        }
    }
}