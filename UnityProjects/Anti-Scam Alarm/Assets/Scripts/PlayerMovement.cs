using UnityEngine;

[RequireComponent(typeof(CharacterController),
                  typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

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
        float horizontalInput = Input.GetAxis(Horizontal);
        float verticalInput = Input.GetAxis(Vertical);

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * _moveSpeed;
        movement = Vector3.ClampMagnitude(movement, _moveSpeed);

        if (movement.magnitude > 0f)
        {
            Quaternion initialOrientation = _camera.rotation;
            _camera.eulerAngles = new Vector3(0, _camera.eulerAngles.y, 0);
            movement = _camera.TransformDirection(movement);
            _camera.rotation = initialOrientation;

            Quaternion direction = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Lerp(transform.rotation, direction, _rotateSpeed * Time.deltaTime);
        }

        _animator.SetFloat(PlayerAnimator.Params.Speed, movement.sqrMagnitude);

        movement *= Time.deltaTime;
        _characterController.Move(movement);
    }
}

public static class PlayerAnimator
{
    public static class Params
    {
        public static int Speed = Animator.StringToHash("Speed");
    }
}