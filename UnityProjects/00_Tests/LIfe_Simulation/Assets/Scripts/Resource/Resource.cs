using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private int _minAmountResource = 1;
    [SerializeField] private int _maxAmountResource= 5;

    public int Amount { get; private set; }

    public void GetAmount()
    {
        Amount = Random.Range(_minAmountResource, _maxAmountResource);
    }
}