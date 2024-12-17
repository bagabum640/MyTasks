using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundDetector;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundDetectorRadius;

    private void Awake()
    {
        if (_groundDetector.TryGetComponent<CircleCollider2D>(out CircleCollider2D collider))
            _groundDetectorRadius = collider.radius;
    }

    public bool IsOnGround =>
         Physics2D.OverlapCircle(_groundDetector.position, _groundDetectorRadius, _groundLayerMask);
}