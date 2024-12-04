using UnityEngine;

public class Medkit : MonoBehaviour, IVisitable
{
    private readonly int _healthCount = 3;

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Use(PlayerHealth playerHealth)
    {
        playerHealth.Restore(_healthCount);
        Destroy(gameObject);
    }
}