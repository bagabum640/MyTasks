using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static Action<Cube> OnTouched;

    private MeshRenderer _cubeMesh;
    private Rigidbody _rigidbody;  
    private bool _isActive;

    private void Awake()
    {
        _cubeMesh = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        const string PlatformTag = "Platform";

        if (_isActive == false && collision.gameObject.CompareTag(PlatformTag))
        {
            _cubeMesh.material.color = GetColor();
            OnTouched?.Invoke(this);
            _isActive = true;
        }
    }

    public void Reset()
    {
        _isActive = false;
        _cubeMesh.material.color = Color.white;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private Color GetColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }
}