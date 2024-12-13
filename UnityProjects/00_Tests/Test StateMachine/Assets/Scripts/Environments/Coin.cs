using UnityEngine;

public class Coin : MonoBehaviour, IVisitable
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Collect()
    {
        Debug.Log("Подобрал монетку");
        Destroy(gameObject);
    }
}