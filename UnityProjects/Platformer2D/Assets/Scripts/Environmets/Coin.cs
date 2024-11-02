using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action OnCreate;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovement>(out _))
        {
            OnCreate?.Invoke();
            Destroy(gameObject);
        }
    }  
}