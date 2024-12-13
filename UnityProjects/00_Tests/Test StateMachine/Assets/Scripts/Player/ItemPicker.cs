using UnityEngine;

public class ItemPicker : MonoBehaviour, IVisitor
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IVisitable>(out IVisitable visitor))
        {
            visitor.Accept(this);
        }
    }

    public void Visit(Medkit medkit)
    {
        PlayerHealth playerHealht = Object.FindObjectOfType<PlayerHealth>();
        medkit.Use(playerHealht);   
    }

    public void Visit(Coin coin)
    {
        coin.Collect();
    }
}