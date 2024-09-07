using System.Collections;
using UnityEngine;

public class SpawnDeterminer : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawnPoints;
    [SerializeField] private float _repeatRate = 2.0f;

    private readonly bool _isWorking = true;

    private void Start()
    {
        if (_spawnPoints.Length > 0)
            StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitForSeconds = new (_repeatRate);

        while (_isWorking)
        {
            _spawnPoints[Random.Range(0, _spawnPoints.Length)].CreateUnit();
            yield return waitForSeconds;
        }
    }
}