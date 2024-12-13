using System;
using UnityEngine;

public class Resourse : MonoBehaviour
{
    private const string PickUp = "PickUp";

    [SerializeField] private int _minAmountGold = 1;
    [SerializeField] private int _maxAmountGold = 5;

    private Animator _animator;

    public int Amount => UnityEngine.Random.Range(_minAmountGold, _maxAmountGold);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        _animator.SetTrigger(PickUp);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}