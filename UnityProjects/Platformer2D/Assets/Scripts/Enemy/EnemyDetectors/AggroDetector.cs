using System;
using UnityEngine;

public class AggroDetector : MonoBehaviour
{
    public event Action IsAggroed;
    public event Action IsExitedAggro;
    public event Action<Transform> IsSetTarget;
    public event Action IsLostTarget;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            if (playerHealth.IsAlive)
            {
                IsAggroed?.Invoke();
                IsSetTarget?.Invoke(playerHealth.transform);
            }
            else
            {
                IsExitedAggro?.Invoke();
                IsLostTarget?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out _))
        {
            IsExitedAggro?.Invoke();
            IsLostTarget?.Invoke();
        }
    }
}