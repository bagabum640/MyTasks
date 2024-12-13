using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _playerAnimator;

    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
    private readonly int Jumping = Animator.StringToHash("Jumping");
    private readonly int Falling = Animator.StringToHash("Falling");
    private readonly int Attacking = Animator.StringToHash("Attacking");
    private readonly int Hurt = Animator.StringToHash("Hurt");
    private readonly int Death = Animator.StringToHash("Death");

    private void Awake() =>
        _playerAnimator = GetComponent<Animator>();

    public void MoveAnimation(float direction) =>
        _playerAnimator.SetFloat(MovementSpeed, Mathf.Abs(direction));

    public void JumpAnimation() =>
        _playerAnimator.SetTrigger(Jumping);

    public void FallAnimation(float velocity) =>
        _playerAnimator.SetFloat(Falling, velocity);

    public void AttackAnimation() =>
        _playerAnimator.SetTrigger(Attacking);

    public void HurtAnimation() =>
        _playerAnimator.SetTrigger(Hurt);

    public void DeathAnimation() =>
        _playerAnimator.SetTrigger(Death);
}