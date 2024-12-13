using UnityEngine;

[RequireComponent(typeof(PlayerGroundDetector),
                  typeof(PlayerMover),
                  typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCombat))]
public class Player : MonoBehaviour
{
    private PlayerGroundDetector _groundCheck;
    private PlayerMover _movement;
    private PlayerCombat _combat;
    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _movement = GetComponent<PlayerMover>();
        _groundCheck = GetComponent<PlayerGroundDetector>();
        _combat = GetComponent<PlayerCombat>();
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.Direction);
        _movement.Fall();

        Attacking();
        JumpDown();
        Jump();
    }

    private void Attacking()
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
}