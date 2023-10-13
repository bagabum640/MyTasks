using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPlace;
    [SerializeField] private GameObject _template;

    private readonly float _delaySpawn = 2f;
    private bool _isWork = true;
    
    private void Start()
    {
        if (_spawnPlace.Length > 0)
        {
            StartCoroutine(SpawnEnemy());
        }        
    }
        
    private IEnumerator SpawnEnemy()
    {
        var waitForSeconds = new WaitForSeconds(_delaySpawn);

        while (_isWork)
        {
            Instantiate(_template, _spawnPlace[Random.Range(0, _spawnPlace.Length)]);

            yield return waitForSeconds;
        }
    }
}