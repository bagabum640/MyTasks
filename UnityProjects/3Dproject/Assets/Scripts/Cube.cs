using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeExplosion))]

public class Cube : MonoBehaviour
{
    [HideInInspector]
    [SerializeField] private float _cloneChance = 100;
    
    private CubeExplosion _explosion;

    public Rigidbody Rigidbody { get; private set; }
    public MeshRenderer MeshRenderer { get; private set; }

    private void OnEnable()
    {
        Rigidbody = GetComponent<Rigidbody>();
        MeshRenderer = GetComponent<MeshRenderer>();
        _explosion = GetComponent<CubeExplosion>();
    }

    private void OnMouseUpAsButton()
    {
        CreateCubes();       
        Destroy(gameObject);
    } 
   
    private void CreateCubes()
    {
        int chancePull = 100;
        
        if (_cloneChance > Random.Range(0, chancePull))
        {
            float cloneChanceDecrease = 0.5f;
            float scaleDecrease = 0.5f;
            int minCubesQuantity = 2;
            int maxCubesQuantity = 6;
            int cubesQuantity = Random.Range(minCubesQuantity, ++maxCubesQuantity);          
            List<Rigidbody> createdCubes = new();
            Cube template = gameObject.GetComponent<Cube>();

            _cloneChance *= cloneChanceDecrease;

            for (int i = 0; i < cubesQuantity; i++)
            {
                Cube cube = Instantiate(template, transform.position, Quaternion.identity);
                cube.transform.localScale = transform.localScale * scaleDecrease;
                ChangeColor(cube.MeshRenderer);               
                createdCubes.Add(cube.Rigidbody);                
            }

            _explosion.BlowUpCubes(createdCubes);
        }
    }

    private void ChangeColor(MeshRenderer cubeMeshRenderer)
    {
        float minColorDeep = 0f;
        float maxColorDeep = 1f;

        cubeMeshRenderer.material.color = new Color(Random.Range(minColorDeep, maxColorDeep),
            Random.Range(minColorDeep, maxColorDeep), Random.Range(minColorDeep, maxColorDeep));
    }
}