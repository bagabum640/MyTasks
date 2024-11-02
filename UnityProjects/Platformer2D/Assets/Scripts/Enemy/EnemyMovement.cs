using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpriteRenderer _spriteRenderer;
    private Vector3[] _points;
    private int _currentPath = 0;

    private void Awake() =>
       _spriteRenderer = GetComponent<SpriteRenderer>();

    private void Start()
    {
        _points = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _points[i] = transform.GetChild(i).position;
        }
    }

    private void Update() =>
        MoveToPoint();

    private void MoveToPoint()
    {
        if (Vector3.Distance(transform.position, _points[_currentPath]) < 0.2f)
            _currentPath = ++_currentPath % _points.Length;

        transform.position = Vector2.MoveTowards(transform.position, _points[_currentPath], _speed * Time.deltaTime);

        Flip();
    }

    private void Flip()
    {
        if (_points[_currentPath].x > transform.position.x)
            _spriteRenderer.flipX = false;
        else if (_points[_currentPath].x < transform.position.x)
            _spriteRenderer.flipX = true;
    }
}