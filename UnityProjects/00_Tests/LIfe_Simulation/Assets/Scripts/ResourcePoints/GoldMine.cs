using UnityEngine;

public class GoldMine : ResourcePoint
{
    [SerializeField] private Golden _goldPrefab;

    private void Awake()
    {
        _resourcePrefab = _goldPrefab;
    }
}