using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [HideInInspector]
    [SerializeField] private float _cloneChance = 100;
    [SerializeField] private float _explosionForce = 100;
    [SerializeField] private float _explosionRadius = 100;

    private void OnMouseUpAsButton() => StartCoroutine(CreateCubes());

    private IEnumerator CreateCubes()
    {
        if (_cloneChance > Random.Range(0, 100))
        {
            float cloneChanceDecrease = 0.5f;
            float scaleDecrease = 0.5f;
            int cubesQuantity = Random.Range(2, 7);
            List<Rigidbody> createdCubes = new();

            _cloneChance *= cloneChanceDecrease;

            for (int i = 0; i < cubesQuantity; i++)
            {
                GameObject cube = Instantiate(gameObject, transform.position, Quaternion.identity);
                cube.transform.localScale = transform.localScale * scaleDecrease;
                ChangeColor(cube.GetComponent<MeshRenderer>());
                createdCubes.Add(cube.GetComponent<Rigidbody>());
                yield return null;
            }

            foreach (Rigidbody createdCube in createdCubes)
            {
                createdCube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                yield return null;
            }
        }

        Destroy(gameObject);
    }

    private void ChangeColor(MeshRenderer cubeMeshRenderer)
    {
        float minColorDeep = 0f;
        float maxColorDeep = 1f;

        cubeMeshRenderer.material.color = new Color(Random.Range(minColorDeep, maxColorDeep),
            Random.Range(minColorDeep, maxColorDeep), Random.Range(minColorDeep, maxColorDeep));
    }
}