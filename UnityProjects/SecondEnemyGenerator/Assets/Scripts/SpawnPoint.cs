using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Target _target;
    [SerializeField] private Unit[] _units;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private int _poolMaxSize = 3;

    private ObjectPool<Unit> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Unit>(
            createFunc: () => Instantiate(_units[Random.Range(0, _units.Length)], transform, true),
            actionOnGet: (unit) => SetUp(unit),
            actionOnRelease: (unit) => ResetUnit(unit),
            actionOnDestroy: (unit) => Destroy(unit.gameObject),
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public void CreateUnit() => _pool.Get();

    private void Release(Unit unit) => _pool.Release(unit);

    private void SetUp(Unit unit)
    {     
        unit.transform.position = transform.position;
        unit.SetTarget(_target);
        unit.gameObject.SetActive(true);
        unit.OnReleased += Release;
    }

    private void ResetUnit(Unit unit)
    {
        unit.gameObject.SetActive(false);
        unit.OnReleased -= Release;
    }        
}