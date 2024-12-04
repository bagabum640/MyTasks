using System;
using UnityEngine;

public class Coin : MonoBehaviour, IVisitable
{
    public event Action<Coin> IsDestroyed;

    public void Accept(IVisitor visitor) =>
        visitor.Visit(this);

    public void Collect()
    {
        IsDestroyed?.Invoke(this);

        Destroy(gameObject);
    }
}