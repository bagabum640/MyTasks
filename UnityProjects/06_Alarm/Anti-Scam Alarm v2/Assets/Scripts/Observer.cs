using System;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public event Action<bool> IsTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out _))
        {
            IsTrigger?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            IsTrigger?.Invoke(false);
        }
    }
}