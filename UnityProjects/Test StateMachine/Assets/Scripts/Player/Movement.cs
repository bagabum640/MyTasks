using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate() =>
        Move();

    private void Move()
    {
        float horizontalInput = Input.GetAxis(Horizontal);
        float verticalInput = Input.GetAxis(Vertical);

        _rigidbody.velocity = new Vector2(horizontalInput, verticalInput) * _speed;
    }
}