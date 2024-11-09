using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public static Animator PlayerAnimator;

    public static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
    public static readonly int Jumping = Animator.StringToHash("Jumping");
    public static readonly int Falling = Animator.StringToHash("Falling");
    public static readonly int Attacking = Animator.StringToHash("Attacking");
    public static readonly int Hurt = Animator.StringToHash("Hurt");
    public static readonly int Death = Animator.StringToHash("Death");

    private void Awake() =>
        PlayerAnimator = GetComponent<Animator>();
}