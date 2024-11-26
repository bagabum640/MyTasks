using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public static Animator PlayerAnimator;

    public static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
    public static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
    public static readonly int Attacking = Animator.StringToHash("Attacking");
    public static readonly int Hurt = Animator.StringToHash("Hurt");
    public static readonly int Death = Animator.StringToHash("Death");

    private void Awake() =>
        PlayerAnimator = GetComponent<Animator>();
}