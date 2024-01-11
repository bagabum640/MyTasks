using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] Player _target;
    
    private float _smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(_target.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime); 
    }
}
