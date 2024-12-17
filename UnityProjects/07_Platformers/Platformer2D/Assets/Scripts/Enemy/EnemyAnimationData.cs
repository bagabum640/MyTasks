using UnityEngine;

public static class EnemyAnimationData
{
    public static readonly int MovementSpeed = Animator.StringToHash("Speed");    
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Hurt = Animator.StringToHash("Hurt");
    public static readonly int Death = Animator.StringToHash("Death");
}