using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),
                  typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _cubeMesh;
    private Rigidbody _rigidbody;
    private bool _isActive;

    public event Action<Cube> OnTouched;

    private void Awake()
    {
        _cubeMesh = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() => Reset();
            
    private void OnCollisionEnter(Collision collision)
    {
        if (_isActive == false && collision.gameObject.TryGetComponent<ColorChanger>(out ColorChanger colorChanger))
        {
            _isActive = true;
            _cubeMesh.material.color = colorChanger.GetColor();
            StartCoroutine(WaitForRelease());
        }
    }

    private IEnumerator WaitForRelease()
    {
        int _minLifeTime = 2;
        int _maxLifeTime = 5;
        int delay = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        yield return new WaitForSeconds(delay);

        OnTouched?.Invoke(this);
    }

    public void Reset()
    {
        _isActive = false;
        _cubeMesh.material.color = Color.white;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}