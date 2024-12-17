using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(Animator))]
public abstract class Unit : MonoBehaviour
{
    private const string Speed = "Speed";

    [SerializeField] private float _speed;
    //[SerializeField] private SelectionBorder _border;

    protected Animator Animator;
    protected NavMeshAgent _agent;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    protected void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _speed;
        // Unselect();
    }

    public void GetPathToMove(Vector3 path)
    {
        if (path != null)
        {
            _agent.SetDestination(path);
            Animator.SetBool("IsWalk", true);
        }
        else
        {
            Animator.SetBool("IsWalk", false);
        }

        GetPathDirection(path);
        //Animator.SetFloat(Speed, Mathf.Abs(new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y).magnitude));
        //_rigidbody.velocity = (path - transform.position).normalized * (_speed * multiplieSpeed);
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