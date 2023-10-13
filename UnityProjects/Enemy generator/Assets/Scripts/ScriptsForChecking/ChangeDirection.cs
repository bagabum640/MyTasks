using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetermineDirection))]

public class ChangeDirection : MonoBehaviour
{
    private DetermineDirection _determineDirection;

    private void Start()
    {
        _determineDirection = GetComponent<DetermineDirection>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
        {
            enemyMovement.SetDirection(_determineDirection.GetDirection());
        }
    }
}
