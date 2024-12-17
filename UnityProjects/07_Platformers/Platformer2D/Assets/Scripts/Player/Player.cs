using UnityEngine;

[RequireComponent(typeof(PlayerGroundDetector),
                  typeof(PlayerMover),
                  typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerCombat))]
public class Player : MonoBehaviour
{
    private PlayerGroundDetector _groundCheck;
    private PlayerMover _movement;
    private PlayerCombat _combat;
    private PlayerHealth _health;
    private PlayerInputReader _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInputReader>();
        _movement = GetComponent<PlayerMover>();
        _groundCheck = GetComponent<PlayerGroundDetector>();
        _combat = GetComponent<PlayerCombat>();
        _health = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        _health.PlayerDied += Die;
    }

    private void OnDisable()
    {
        _health.PlayerDied -= Die;
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.Direction);
        _movement.Fall();

        Attack();
        JumpDown();
        Jump();
    }

    private void Attack()
    {
        if(_input.GetIsAttack() && _groundCheck.IsOnGround)
            _combat.AttackDelay();
    }

    private void JumpDown()
    {
        if(_input.GetIsJumpDown() && _groundCheck.IsOnGround)
            _movement.JumpDown();
    }

    private void Jump()
    {
        if (_input.GetIsJump() && _groundCheck.IsOnGround)
            _movement.Jump();
    }

    private void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Player>().enabled = false;       
    }
}