using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundCheckRadius;

    public bool OnGround { get; private set; }

    private void Start() =>
        _groundCheckRadius = _groundCheckPoint.GetComponent<CircleCollider2D>().radius;

    private void Update() =>
        GroundCheck();

    private void GroundCheck() =>
        OnGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayerMask);
}