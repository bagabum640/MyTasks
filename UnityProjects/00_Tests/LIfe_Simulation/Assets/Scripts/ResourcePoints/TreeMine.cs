using UnityEngine;

public class TreeMine : ResourcePoint
{
    [SerializeField] private Wood _treePrefab;

    private void Awake()
    {
        _resourcePrefab = _treePrefab;
    }
}