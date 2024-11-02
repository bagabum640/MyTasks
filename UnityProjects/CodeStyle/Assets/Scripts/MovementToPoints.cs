using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Vector3[] _points;
    private int _pointNumber = 0;

    private void Start()
    {
        _points = new Vector3[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i).position;
    }

    private void FixedUpdate() =>
        MoveToPoint();

    private void MoveToPoint()
    {
        if (transform.position == _points[_pointNumber])
            _pointNumber = ++_pointNumber % _points.Length;

        transform.position = Vector3.MoveTowards(transform.position, _points[_pointNumber], _speed * Time.deltaTime);
    }
}