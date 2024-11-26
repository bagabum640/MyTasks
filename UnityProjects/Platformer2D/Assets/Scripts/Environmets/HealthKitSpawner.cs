using UnityEngine;
using System.Collections;

public class HealthKitSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private HealthKit _healthKitPrefab;

    private void Awake()
    {
        Init();
    }

    private IEnumerator SpawnCoin(Vector2 position, float firstSpawnDelay = 0.1f)
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        Instantiate(_healthKitPrefab, position, Quaternion.identity, transform);
    }

    private void Init()
    {
        if (_spawnPoints.Length > 0)
            for (int i = 0; i < transform.childCount; i++)
            {
                StartCoroutine(SpawnCoin(_spawnPoints[i].position));
            }
    }
}