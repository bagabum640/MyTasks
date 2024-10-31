using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private CharacterController _characterController;
    private Animator _animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() =>
        Move();

    private void Move()
    {
        Vector3 movement = Vector3.zero;

        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            movement.x = horizontalInput * _moveSpeed;
            movement.z = verticalInput * _moveSpeed;
            movement = Vector3.ClampMagnitude(movement, _moveSpeed);

            Quaternion initialOrientation = _camera.rotation;
            _camera.eulerAngles = new Vector3(0, _camera.eulerAngles.y, 0);
            movement = _camera.TransformDirection(movement);
            _camera.rotation = initialOrientation;

            Quaternion direction = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Lerp(transform.rotation, direction, _rotateSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        movement *= Time.deltaTime;
        _characterController.Move(movement);
    }
}