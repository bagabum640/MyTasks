using UnityEngine;

public class HealthKit : MonoBehaviour,ICollectable
{
    [field: SerializeField] public int HealthAmount { get; private set; } = 3;
}