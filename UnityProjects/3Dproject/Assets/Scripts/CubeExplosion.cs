using UnityEngine;
using System.Collections.Generic;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 100;
    [SerializeField] private float _explosionRadius = 100;

    public void BlowUpCubes(List<Rigidbody> cubes)
    {
        foreach (Rigidbody cube in cubes)
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}