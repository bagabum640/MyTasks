using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpriteRenderer _spriteRenderer;
    private Target _target;

    private void Awake() =>
        _spriteRenderer = GetComponent<SpriteRenderer>();

    private void FixedUpdate() =>
        Move();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Target>(out _) == true)
            Destroy(this.gameObject);
    }

    public void SetTarget(Target target) => 
        _target = target;

    private void Move()
    {
        Flip();

        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x < _target.transform.position.x)
            _spriteRenderer.flipX = false;
        else if (transform.position.x > _target.transform.position.x)
            _spriteRenderer.flipX = true;
    }
}