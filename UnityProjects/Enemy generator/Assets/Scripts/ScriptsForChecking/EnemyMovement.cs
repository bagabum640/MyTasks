using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 150;

    private Rigidbody2D _rigidBody;
    private Animator _animator;    
    private float _directionX;
    private float _directionY;

    public void SetDirection(Vector2 direction)
    {
        _directionX = direction.x;
        _directionY = direction.y;
    }

    public void Die()
    {
        _animator.SetTrigger(AnimatorEnemyController.Params.Death);

        StartCoroutine(Delete());
    }

    public void Win()
    {
        _animator.SetTrigger(AnimatorEnemyController.Params.Win);

        StartCoroutine(Delete());
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
        
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidBody.velocity = _speed * Time.fixedDeltaTime * new Vector2(_directionX, _directionY);

        float speed = _rigidBody.velocity.magnitude;

        _animator.SetFloat(AnimatorEnemyController.Params.Horizontal, _directionX);
        _animator.SetFloat(AnimatorEnemyController.Params.Vertical, _directionY);
        _animator.SetFloat(AnimatorEnemyController.Params.Speed, speed);
    }

    private void Stop()
    {
        _directionX = 0;
        _directionY = 0;        
    }

    private IEnumerator Delete()
    {
        Stop();
        
        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
    }
}

public static class AnimatorEnemyController
{
    public static class Params
    {
        public static readonly int Horizontal = Animator.StringToHash("Horizontal");
        public static readonly int Vertical = Animator.StringToHash("Vertical");
        public static readonly int Speed = Animator.StringToHash("Speed");
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Win = Animator.StringToHash("Win");
    }
}