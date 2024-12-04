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

    private void OnEnable()
    {
        _input.Jumped += Jump;
        _input.JumpedOff += JumpOff;
        _input.Attacking += Attacking;
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.Direction);
        _movement.Fall();
    }

    private void OnDisable()
    {
        _input.Jumped -= Jump;
        _input.JumpedOff -= JumpOff;
        _input.Attacking -= Attacking;
    }

    private void Attacking()
    {
        if(_groundCheck.IsOnGround)
            _combat.AttackDelay();
    }

    private void JumpOff()
    {
        if(_groundCheck.IsOnGround)
            _movement.JumpOff();
    }

    private void Jump()
    {
        if (_groundCheck.IsOnGround)
            _movement.Jump();
    }
}