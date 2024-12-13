using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 100;
    [SerializeField] private float _explosionRadius = 10;

    public void Push(List<Rigidbody> cubes)
    {
        foreach (Rigidbody cube in cubes)
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    public void BlowUp(float scaleFactor)
    {
        foreach (Rigidbody explodableObject in GetExplodableObject(scaleFactor))
            explodableObject.AddExplosionForce(_explosionForce / scaleFactor, transform.position, _explosionRadius / scaleFactor);
    }

    private List<Rigidbody> GetExplodableObject(float scaleFactor)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius / scaleFactor);

        List<Rigidbody> explodableObjects = new List<Rigidbody>();

        foreach (Collider collider in colliders)
            if (collider.attachedRigidbody != null)
                explodableObjects.Add(collider.attachedRigidbody);

        return explodableObjects;
    }
}