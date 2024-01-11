using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    public static UnityEvent<Transform> GemCollected = new();

    public static void SendGemCollected(Transform parentTransform)
    {                
        GemCollected?.Invoke(parentTransform);       
    }
}
