using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnRate;

    private readonly bool _isWork = true;

    private void Start() 
    {
        if (_spawnPoints.Length > 0)
            StartCoroutine(Spawn()); 
    }

    private IEnumerator Spawn()
    {
        while (_isWork)
        {
            WaitForSeconds waitForSeconds = new(_spawnRate);
            SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            spawnPoint.SetTarget(Instantiate(spawnPoint.Unit, spawnPoint.transform));

            yield return waitForSeconds;
        }
    }
}