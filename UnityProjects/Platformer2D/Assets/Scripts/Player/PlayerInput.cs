using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    private bool _isJump;
    private bool _isJumpOff;
    private bool _isAttacking;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        JumpInput();
        JumpOffInput();
        AttackInput();
    }

    public bool GetJumpSignal() => GetTrigger(ref _isJump);

    public bool GetJumpOffSignal() => GetTrigger(ref _isJumpOff);

    public bool GetAttackSignal() => GetTrigger(ref _isAttacking);

    private void AttackInput()
    {
        if (Input.GetMouseButtonDown(0))
            _isAttacking = true;
    }

    private void JumpOffInput()
    {
        if(Input.GetKeyDown(KeyCode.S))
            _isJumpOff = true;
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _isJump = true;
    }

    private bool GetTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}