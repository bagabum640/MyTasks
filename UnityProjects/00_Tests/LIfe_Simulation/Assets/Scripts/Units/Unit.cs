using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(Animator))]
public abstract class Unit : MonoBehaviour
{
    private const string Speed = "Speed";

    [SerializeField] private float _speed;
    //[SerializeField] private SelectionBorder _border;

    protected Animator Animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

       // Unselect();
    }

    public void GetPathToMove(Vector3 path, float multiplieSpeed = 1f)
    {       
        GetPathDirection(path);
        _rigidbody.velocity = (path - transform.position).normalized * (_speed * multiplieSpeed);
        Animator.SetFloat(Speed, Mathf.Abs(new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y).magnitude));
    }

    public void GetPathDirection(Vector3 path) =>
        _spriteRenderer.flipX = (path - transform.position).x < 0;

    public void ResetSpeed() =>
        _rigidbody.velocity = Vector3.zero;

    //public void Select()
    //{
    //    _border.gameObject.SetActive(true);
    //}

    //public void Unselect()
    //{
    //    _border.gameObject.SetActive(false);
    //}
}