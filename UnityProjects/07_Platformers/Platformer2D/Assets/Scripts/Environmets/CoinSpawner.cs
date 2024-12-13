using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Coin _coinPrefab;

    private Coin _coin;

    private void Awake() =>   
        Init();   

    private IEnumerator SpawnCoin(Vector2 position, float firstSpawnDelay = 0.1f)
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        CreateCoin(position);
    }

    private void Init()
    {
        if (_spawnPoints.Length > 0)
            for (int i = 0; i < _spawnPoints.Length; i++)
                CreateCoin(_spawnPoints[i].position);
    }

    public void Create(Coin coin)
    {
        float spawnDelay = 1f;
        StartCoroutine(SpawnCoin(coin.transform.position, spawnDelay));
        coin.IsDestroyed -= Create;
    }

    private void CreateCoin(Vector2 position)
    {
        _coin = Instantiate(_coinPrefab, position, Quaternion.identity, transform);
        _coin.IsDestroyed += Create;
    }
}