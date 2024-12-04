using UnityEngine;

public class PlayerItemPicker : MonoBehaviour, IVisitor
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IVisitable>(out IVisitable item))
        {
            item.Accept(this);
        }
    }

    public void Visit(HealthKit healthKit)
    {
        PlayerHealth playerHealth = Object.FindObjectOfType<PlayerHealth>();
        healthKit.Use(playerHealth);
    }

    public void Visit(Coin coin)
    {
        coin.Collect();
    }
}