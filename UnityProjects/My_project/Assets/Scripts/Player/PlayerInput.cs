using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";

    public Vector2 Direction { get; private set; }

    private void Update()
    {
        float horizontalInput = Input.GetAxis(Horizontal);
        float verticalInput = Input.GetAxis(Vertical);

        Direction = new Vector2(horizontalInput, verticalInput);
    }
}