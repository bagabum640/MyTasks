using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class SlimeSaliva : MonoBehaviour
{
    [SerializeField] Vector2 _shotForce;

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Health.TakeDamage();
        }

        Destroy(gameObject);
    }

    public void SetDirection(Vector2 direction)
    {
        _shotForce = (new(direction.x * _shotForce.x, _shotForce.y));
        Fly();
    }

    private void Fly() => _rb2D.AddForce(_shotForce, ForceMode2D.Impulse);
}
