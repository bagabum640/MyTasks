using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private Target _target;

    public Unit Unit => _unit;

    public void SetTarget(Unit unit) => unit.SetTarget(_target);
}