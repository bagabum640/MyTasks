using UnityEngine;
using static PlayerAnimator;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool _isFlipped = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void Move(float direction)
    {
        if ((direction > 0 && !_isFlipped) || (direction < 0 && _isFlipped))
        {
            transform.localScale *= new Vector2(-1f, 1f);
            _isFlipped = !_isFlipped;
        }

        _rigidbody.velocity = new Vector2(direction * _speed, _rigidbody.velocity.y);

        _animator.SetFloat(MovementSpeed, Mathf.Abs(direction));
    }

    public void Jump()
    {
        _rigidbody.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
        _animator.SetTrigger(Jumping);
    }

    public void Fall() =>
        _animator.SetFloat(Falling, _rigidbody.velocity.y);
}