using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    private const string MouseX = "Mouse X";

    [SerializeField] private Transform _player;
    [SerializeField, Range(1, 5)] private float _rotationSpeed;

    private Vector3 _offset;
    private float _rotationY;

    private void Start()
    {
        _rotationY = transform.eulerAngles.y;
        _offset = _player.position - transform.position;
    }

    private void LateUpdate()
    {
        _rotationY += Input.GetAxis(MouseX) * _rotationSpeed;

        Quaternion rotationAxis = Quaternion.Euler(0, _rotationY, 0);
        transform.position = _player.position - (rotationAxis * _offset);
        transform.LookAt(_player);
    }
}