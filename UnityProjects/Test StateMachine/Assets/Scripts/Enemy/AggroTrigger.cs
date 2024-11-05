using UnityEngine;

public class AggroTrigger : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake() =>
        _enemy = GetComponentInParent<Enemy>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out _))
            _enemy.SetAggroStatus(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out _))
            _enemy.SetAggroStatus(false);
    }
}