using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private Vector2[] _spawners;
    private int _spawnCount;

    private void Start()
    {
        _spawners = new Vector2[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _spawners[i] = transform.GetChild(i).position;
            StartCoroutine(SpawnCoin(_spawners[i]));
        }
    }

    private void Create(Vector2 position)
    {
        float spawnDelay = 1f;

        StartCoroutine(SpawnCoin(position, spawnDelay));
    }

    private IEnumerator SpawnCoin(Vector2 position, float firstSpawnDelay = 0.1f)
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        Coin coin = Instantiate(_coin, position, Quaternion.identity);

        coin.OnCreate += Create;
    }
}