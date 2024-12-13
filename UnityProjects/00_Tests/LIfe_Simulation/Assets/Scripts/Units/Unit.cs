using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(Animator))]
public abstract class Unit : MonoBehaviour
{
    private const string Speed = "Speed";

    [SerializeField] private float _speed;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private bool _isFlipped = true;   

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected void Move(Vector2 direction) 
    {
        if ((direction.x > 0 && _isFlipped == false) || (direction.x < 0 && _isFlipped == true))
        {
            transform.localScale *= new Vector2(-1f, 1f);
            _isFlipped = !_isFlipped;
        }

        transform.position = Vector2.MoveTowards(transform.position, direction, _speed * Time.deltaTime);
        //_rigidbody.velocity = direction * _speed;
        _animator.SetFloat(Speed, Mathf.Abs(direction.x));
    }
}