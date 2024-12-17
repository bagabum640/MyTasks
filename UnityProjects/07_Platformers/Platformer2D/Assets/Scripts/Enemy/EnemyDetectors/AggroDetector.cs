using System;
using System.Collections;
using UnityEngine;

public class AggroDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private float _delay = 0.2f;

    private readonly bool _isWork = true;

    public event Action<Transform> TargetFound;
    public event Action TargetLost;

    private void Start() =>
        StartCoroutine(FindTarget());

    private IEnumerator FindTarget()
    {
        float angleBox = 0f;

        WaitForSeconds waitForSeconds = new(_delay);
        Collider2D hitCollider;

        while (_isWork)
        {
            hitCollider = Physics2D.OverlapBox(transform.position, transform.localScale, angleBox, _playerLayerMask);

            if (hitCollider != null)
                TargetFound?.Invoke(hitCollider.transform);
            else
                TargetLost?.Invoke();

            yield return waitForSeconds;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}