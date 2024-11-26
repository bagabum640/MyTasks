using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundDetector;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundDetectorRadius;

    private void Awake() =>
        _groundDetectorRadius = _groundDetector.GetComponent<CircleCollider2D>().radius;

    public bool IsOnGround =>
         Physics2D.OverlapCircle(_groundDetector.position, _groundDetectorRadius, _groundLayerMask);
}