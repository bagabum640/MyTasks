using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class KillEnemy : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
        {
            enemyMovement.Die();
        }
    }
}
