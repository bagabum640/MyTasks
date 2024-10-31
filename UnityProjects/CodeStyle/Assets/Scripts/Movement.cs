using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _pointNumber = 0;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i).GetComponent<Transform>();
    }

    private void FixedUpdate() =>
        MoveToPoint();

    private void MoveToPoint()
    {
        if (_pointNumber == _points.Length)
            _pointNumber = 0;

        transform.position = Vector3.MoveTowards(transform.position, _points[_pointNumber].position, _speed * Time.deltaTime);

        if (transform.position == _points[_pointNumber].position)
            _pointNumber++;
    }
}