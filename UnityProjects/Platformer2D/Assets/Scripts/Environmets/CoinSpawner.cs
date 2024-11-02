using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private void Start() =>
        StartCoroutine(SpawnCoin());

    private void Create()
    {
        float spawnDelay = 1f;

        StartCoroutine(SpawnCoin(spawnDelay));
    }

    private IEnumerator SpawnCoin(float firstSpawnDelay = 0.1f)
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        Coin coin = Instantiate(_coin, transform.position, Quaternion.identity);

        coin.OnCreate += Create;
    }
}