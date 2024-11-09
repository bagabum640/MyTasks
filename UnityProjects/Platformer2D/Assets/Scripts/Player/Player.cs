using UnityEngine;

[RequireComponent(typeof(PlayerGroundDetector),
                  typeof(PlayerMovement),
                  typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCombat))]
public class Player : MonoBehaviour
{
    private PlayerGroundDetector _groundCheck;
    private PlayerMovement _movement;
    private PlayerCombat _combat;
    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _movement = GetComponent<PlayerMovement>();
        _groundCheck = GetComponent<PlayerGroundDetector>();
        _combat = GetComponent<PlayerCombat>();
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.Direction);
        _movement.Fall();

        if (_input.GetJumpSignal() && _groundCheck.IsGround)
            _movement.Jump();

        if (_input.GetJumpOffSignal() && _groundCheck.IsGround)
            _movement.JumpOffPlatform();

        if(_input.GetAttackSignal() && _groundCheck.IsGround)
            _combat.AttackDelay();
    }
}