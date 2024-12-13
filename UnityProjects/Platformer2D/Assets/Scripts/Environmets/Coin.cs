using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public event Action<Coin> IsDestroyed;

    public void Collect()
    {
        IsDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}