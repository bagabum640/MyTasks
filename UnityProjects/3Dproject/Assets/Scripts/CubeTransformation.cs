using UnityEngine;

public class CubeTransformation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _growingSpeed;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward, Space.Self);
        transform.Rotate(_rotateSpeed * Time.deltaTime * Vector3.up);
        transform.localScale += _growingSpeed * Time.deltaTime * Vector3.one;
    }
}