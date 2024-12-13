using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private DirectionSetter[] _spawnPlace;
    [SerializeField] private Enemy _template;

    private readonly float _delay = 2f;
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
        var waitForSeconds = new WaitForSeconds(_delay);

        while (_isWork)
        {
            Instantiate(_template, _spawnPlace[Random.Range(0, _spawnPlace.Length)].transform);
            
            yield return waitForSeconds;
        }
    }
}