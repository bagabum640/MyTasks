using System;
using UnityEngine;

public class HealthKit : MonoBehaviour, ICollectable, IHealable
{
    //[SerializeField] private float _healthRestoreCount = 3;

    public float _healCount { get; set; } = 3f;

    public static event Action<float> IsHealing;

    public void Collect()
    {
        IsHealing?.Invoke(_healCount);

        Destroy(gameObject);
    }
}