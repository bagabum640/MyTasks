using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpForce = 18f;

    private readonly string _groundTag = "Ground";
    private readonly int _playerLayer = 3;
    private readonly int _platformLayer = 6;

    private readonly KeyCode _jumpKey = KeyCode.Space;
    private readonly KeyCode _jumpDownKey = KeyCode.S;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private float _moveDirection;
    private bool _isGrounded = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetDirection();
        _animator.SetFloat(PlayerAnimator.Params.SpeedUp, _rigidbody2D.velocity.y);

        if (_isGrounded && Input.GetKeyDown(_jumpKey))        
            Jump();

        if (Input.GetKeyDown(_jumpDownKey))
            StartCoroutine(JumpDown());
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody2D.velocity = new(_moveDirection * _speed, _rigidbody2D.velocity.y);
    }

    private void SetDirection()
    {
        _moveDirection = Input.GetAxis("Horizontal");
        _animator.SetFloat(PlayerAnimator.Params.Speed, Mathf.Abs(_moveDirection));

        if (_moveDirection > 0)
            _spriteRenderer.flipX = false;
        else if (_moveDirection < 0)
            _spriteRenderer.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
            _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
            _isGrounded = false;
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    private IEnumerator JumpDown()
    {
        WaitForSeconds waitForSeconds = new(0.5f);
        Physics2D.IgnoreLayerCollision(_playerLayer, _platformLayer, true);

        yield return waitForSeconds;
        Physics2D.IgnoreLayerCollision(_playerLayer, _platformLayer, false);
    }
}
