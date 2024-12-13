using UnityEngine;
using System.Collections;

public class GemsSpawner : MonoBehaviour
{
    [SerializeField] private Gem _template;
    [SerializeField] private Transform[] _spawnPositions;
        
    private void Start()
    {        
        Events.GemCollected.AddListener(StartSpawnGem);
        
        if (_spawnPositions.Length > 0)
        {
            foreach (var spawnPosition in _spawnPositions)                
                StartCoroutine(SpawnGem(spawnPosition, 0.5f));
        }        
    }

    private void StartSpawnGem(Transform pos)
    {
        StartCoroutine(SpawnGem(pos));
    }

    private IEnumerator SpawnGem(Transform parent, float delay = 3f)
    {       
        WaitForSeconds waitForSeconds = new(delay);
        yield return waitForSeconds;

        Instantiate(_template, parent.position, Quaternion.identity, parent);
    }
}
