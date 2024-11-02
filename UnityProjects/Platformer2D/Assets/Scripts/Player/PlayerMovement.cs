using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(Animator),
                  typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private PlayerGroundCheck _check;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _movement;

    private void Awake()
    {
        _check = GetComponent<PlayerGroundCheck>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Jump();
        Fall();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _movement.x = Input.GetAxis(Horizontal);

        _rigidbody.velocity = new Vector2(_movement.x * _speed, _rigidbody.velocity.y);

        _animator.SetFloat("Speed", Mathf.Abs(_movement.x));

        Flip();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _check.OnGround)
        {
            _rigidbody.AddForce(_jumpForce * Vector2.up,ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    private void Fall()
    {
        _animator.SetFloat("AirSpeedY", _rigidbody.velocity.y);
    }

    private void Flip()
    {
        if ((_movement.x > 0))
        {
            _spriteRenderer.flipX = false;
        }
        else if ((_movement.x < 0))
        {
            _spriteRenderer.flipX = true;
        }
    }
}