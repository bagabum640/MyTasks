using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator _playerAnimator;

    public readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
    public readonly int Jumping = Animator.StringToHash("Jumping");
    public readonly int Falling = Animator.StringToHash("Falling");
    public readonly int Attacking = Animator.StringToHash("Attacking");
    public readonly int Hurt = Animator.StringToHash("Hurt");
    public readonly int Death = Animator.StringToHash("Death");

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