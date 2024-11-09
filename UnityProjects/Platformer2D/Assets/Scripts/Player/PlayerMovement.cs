using System.Collections;
using UnityEngine;
using static PlayerAnimations;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private const int PlayerMask = 7;
    private const int PlatformMask = 6;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpOffDelay;

    private Rigidbody2D _rigidbody;

    private bool _isFlipped = true;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody2D>();

    public void Move(float direction)
    {
        if ((direction > 0 && !_isFlipped) || (direction < 0 && _isFlipped))
        {
            transform.localScale *= new Vector2(-1f, 1f);
            _isFlipped = !_isFlipped;
        }

        _rigidbody.velocity = new Vector2(direction * _speed, _rigidbody.velocity.y);

        PlayerAnimator.SetFloat(MovementSpeed, Mathf.Abs(direction));
    }
    private IEnumerator JumpOff()
    {
        Physics2D.IgnoreLayerCollision(PlayerMask, PlatformMask, true);

        yield return new WaitForSeconds(_jumpOffDelay);

        Physics2D.IgnoreLayerCollision(PlayerMask, PlatformMask, false);
    }

    public void Jump()
    {
        _rigidbody.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
        PlayerAnimator.SetTrigger(Jumping);
    }

    public void Fall() =>
        PlayerAnimator.SetFloat(Falling, _rigidbody.velocity.y);

    public void JumpOffPlatform() =>
        StartCoroutine(JumpOff());
}