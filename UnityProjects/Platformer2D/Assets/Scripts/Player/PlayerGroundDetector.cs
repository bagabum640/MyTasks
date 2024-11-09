using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundCheckRadius;

    public bool IsGround { get; private set; }

    private void Awake() =>
        _groundCheckRadius = _groundCheckPoint.GetComponent<CircleCollider2D>().radius;

    private void Update() =>
        GroundDetect();

    private void GroundDetect() =>
        IsGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayerMask);
}