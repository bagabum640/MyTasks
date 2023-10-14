using UnityEngine;

[RequireComponent(typeof(DirectionDeterminer))]

public class DirectionSetter : MonoBehaviour
{
    private DirectionDeterminer _directionDeterminer;

    private void Start()
    {
        _directionDeterminer = GetComponent<DirectionDeterminer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemyMovement))
        {
            enemyMovement.SetDirection(_directionDeterminer.GetDirection());
        }
    }
}
