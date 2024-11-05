using UnityEngine;

public static class PlayerAnimator
{
    public static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
    public static readonly int Jumping = Animator.StringToHash("Jumping");
    public static readonly int Falling = Animator.StringToHash("Falling");
    public static readonly int Attacking = Animator.StringToHash("Attacking");
}