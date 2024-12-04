using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Coin _coinPrefab;

    private void Awake() =>   
        Init();   

    private IEnumerator SpawnCoin(Vector2 position, float firstSpawnDelay = 0.1f)
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        Coin coin = Instantiate(_coinPrefab, position, Quaternion.identity, transform);

        coin.IsDestroyed += Create;
    }

    private void Init()
    {
        if (_spawnPoints.Length > 0)
            for (int i = 0; i < transform.childCount; i++)
            {
                StartCoroutine(SpawnCoin(_spawnPoints[i].position));
            }
    }

    public void Create(Coin coin)
    {
        float spawnDelay = 1f;
        StartCoroutine(SpawnCoin(coin.transform.position, spawnDelay));
        coin.IsDestroyed -= Create;
    }
}