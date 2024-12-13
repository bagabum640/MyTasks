using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 150;

    private Rigidbody2D _rigidBody;
    private Animator _animator;    
    private float _directionX;
    private float _directionY;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetDirection(Vector2 direction)
    {
        _directionX = direction.x;
        _directionY = direction.y;
    }

    public void Die()
    {
        _animator.SetTrigger(CommonSoldier.Params.Death);

        StartCoroutine(Delete());
    }

    public void Win()
    {
        _animator.SetTrigger(CommonSoldier.Params.Win);

        StartCoroutine(Delete());
    }

    private void Move()
    {
        _rigidBody.velocity = _speed * Time.fixedDeltaTime * new Vector2(_directionX, _directionY);

        float speed = _rigidBody.velocity.magnitude;

        _animator.SetFloat(CommonSoldier.Params.Horizontal, _directionX);
        _animator.SetFloat(CommonSoldier.Params.Vertical, _directionY);
        _animator.SetFloat(CommonSoldier.Params.Speed, speed);
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

public static class CommonSoldier
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