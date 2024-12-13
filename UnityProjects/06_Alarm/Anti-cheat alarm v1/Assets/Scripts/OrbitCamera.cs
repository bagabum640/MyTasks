using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 2f;
   
    private Vector3 _offset;
    private float _rotationX;
    private float _rotationY;
    private float _maxVertical = 45f;
    private float _minVertical = -45f;
    
    private void Start()
    {        
        _offset = _target.position - transform.position;
    }

    private void LateUpdate()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _rotationSpeed;
        _rotationY += Input.GetAxis("Mouse X") * _rotationSpeed;
        _rotationX = Mathf.Clamp(_rotationX, _minVertical, _maxVertical);
        
        Quaternion rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        
        transform.position = _target.position - (rotation * _offset);
        
        transform.LookAt(_target);
    }
}
