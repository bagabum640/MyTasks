using UnityEngine;

public class FoodMine : ResourcePoint
{
    [SerializeField] private Food _foodPrefab;

    private void Awake()
    {
        _resourcePrefab = _foodPrefab;
    }
}