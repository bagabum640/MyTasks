using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),                 
                  typeof(PlayerAnimations))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private List<int> _layerID;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpOffDelay;

    private PlayerAnimations _playerAnimation;
    private Rigidbody2D _rigidbody;
    private WaitForSeconds _waitForSeconds;
    private bool _isFlipped = true;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimations>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _waitForSeconds = new(_jumpOffDelay);
    }

    private IEnumerator HandlingJumpDown()
    {
        IgnoreLayers(true);

        yield return _waitForSeconds;

        IgnoreLayers(false);
    }

    public void Move(float direction)
    {
        if ((direction > 0 && _isFlipped == false) || (direction < 0 && _isFlipped == true))
        {
            transform.localScale *= new Vector2(-1f, 1f);
            _isFlipped = !_isFlipped;
        }

        _rigidbody.velocity = new Vector2(direction * _moveSpeed, _rigidbody.velocity.y);

        _playerAnimation.MoveAnimation(direction);
    }

    public void Jump()
    {       
        _rigidbody.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
        _playerAnimation.JumpAnimation();
    }

    public void Fall() =>
        _playerAnimation.FallAnimation(_rigidbody.velocity.y);

    public void JumpDown() =>
        StartCoroutine(HandlingJumpDown());

    private void IgnoreLayers(bool isIgnore)
    {
        foreach (int layerID in _layerID)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, layerID, isIgnore);
        }
    }
}