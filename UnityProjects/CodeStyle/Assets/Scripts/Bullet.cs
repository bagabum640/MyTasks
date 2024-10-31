using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate() =>
        Fly();

    public void SetDirection(Vector3 direction) =>
        _direction = direction;

    private void Fly() =>   
        _rigidbody.velocity = _direction * _speed; 
}