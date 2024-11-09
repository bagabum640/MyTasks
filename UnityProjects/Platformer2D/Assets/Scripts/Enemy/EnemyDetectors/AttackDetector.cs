using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake() =>
        _enemy = GetComponentInParent<Enemy>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            _enemy.SetFightStatus(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            _enemy.SetFightStatus(false);
        }
    }
}