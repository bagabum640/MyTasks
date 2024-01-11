using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]

public class Enemy : MonoBehaviour
{
    private string _layer = "Enemy";

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(_layer);
    }
}