using UnityEngine;

public class AggroDetector : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()  =>
        _enemy = GetComponentInParent<Enemy>();   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _enemy.SetTarget(player.transform);
            _enemy.SetAggroStatus(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            _enemy.SetTarget(null);
            _enemy.SetAggroStatus(false);
        }
    }
}