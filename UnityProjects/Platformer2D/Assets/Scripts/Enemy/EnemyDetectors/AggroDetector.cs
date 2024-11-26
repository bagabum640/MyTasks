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
        if (collision.TryGetComponent<Player>(out Player player))
        {
            IsAggroed?.Invoke();
            IsSetTarget?.Invoke(player.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            IsExitedAggro?.Invoke();
            IsLostTarget?.Invoke();
        }
    }
}