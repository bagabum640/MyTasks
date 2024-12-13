using UnityEngine;

public class HealthKitSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private HealthKit _healthKitPrefab;

    private void Awake() =>
        Init();

    private void Init()
    {
        if (_spawnPoints.Length > 0)
            for (int i = 0; i < _spawnPoints.Length; i++)            
                Instantiate(_healthKitPrefab, _spawnPoints[i].position, Quaternion.identity, transform);            
    }
}