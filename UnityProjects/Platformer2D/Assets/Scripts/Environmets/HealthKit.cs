using UnityEngine;

public class HealthKit : MonoBehaviour, IVisitable
{
    [SerializeField] private float _healCount = 3;

    public void Accept(IVisitor visitor) =>    
        visitor.Visit(this);   

    public void Use(PlayerHealth playerHealth)
    {
        if (playerHealth.GetPossibleOfHealing())
        {
            playerHealth.RestoreHealth(_healCount);

            Destroy(gameObject);
        }
    }
}