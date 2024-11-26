using UnityEngine;
using static PlayerAnimations;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private bool _isFlipped = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(direction.x, direction.y) * _moveSpeed;

        if (_rigidbody.velocity != Vector2.zero)
        {
            PlayerAnimator.SetBool("IsWalking", true);
            PlayerAnimator.SetFloat(HorizontalSpeed, _rigidbody.velocity.x);
            PlayerAnimator.SetFloat(VerticalSpeed, _rigidbody.velocity.y);
            _spriteRenderer.flipX = direction.x < 0;
        }
        else
        {
            PlayerAnimator.SetBool("IsWalking", false);
        }
    }
}