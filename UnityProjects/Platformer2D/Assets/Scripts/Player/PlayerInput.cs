using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    private bool _isJump;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        JumpInput();
    }

    public bool GetJumpSignal() => GetTrigger(ref _isJump);

    private void JumpInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            _isJump = true;
    }

    private bool GetTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}