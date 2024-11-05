using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Vector3 _direction;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate() =>
        Fly();

    public void SetDirection(Vector3 direction) =>
        _direction = direction;

    private void Fly() =>
        _rigidbody.velocity = _direction * _speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Movement>(out _))
            Destroy(gameObject);
    }
}