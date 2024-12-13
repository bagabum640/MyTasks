using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]

public class Player : MonoBehaviour
{    
    public PlayerHealth Health { get; private set; }
    
    private void Start()
    {
        Health = GetComponent<PlayerHealth>();       
    }
}

public static class PlayerAnimator
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash("Speed");
        public static readonly int SpeedUp = Animator.StringToHash("SpeedUp");
        public static readonly int Hurt = Animator.StringToHash("Hurt");
    }
}
