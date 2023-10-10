using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private Transform _target;

    private CharacterController _charController;
    private Vector3 _moveDirection;
    private Animator _animator;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Quaternion targetRotation = _target.rotation;

        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _target.eulerAngles = new Vector3(0f, _target.eulerAngles.y, 0f);
        _moveDirection = _target.TransformDirection(_moveDirection);
        _target.rotation = targetRotation;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _charController.Move(_speed * Time.fixedDeltaTime * _moveDirection);
        _animator.SetFloat("Speed", _speed * _moveDirection.magnitude);

        if (_moveDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDirection), Time.deltaTime * _rotationSpeed);
        }
    }
}
