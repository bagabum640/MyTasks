using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    public event Action Attacking;
    public event Action Jumped;
    public event Action JumpedOff;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        JumpInput();
        JumpOffInput();
        AttackInput();
    }

    private void AttackInput()
    {
        if (Input.GetMouseButtonDown(0))
            Attacking?.Invoke();  
    }

    private void JumpOffInput()
    {
        if(Input.GetKeyDown(KeyCode.S))            
            JumpedOff?.Invoke();
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jumped?.Invoke();
    }
}