using UnityEngine;

public class SphereMove : MonoBehaviour
{
    private void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime;        
    }
}