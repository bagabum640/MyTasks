using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    private readonly int _maxAmount = 100;
    private readonly int _minAmount = 0;

    [SerializeField] protected Slider _slider;
    [SerializeField] protected Text _text;

    private void Awake()
    {
        _slider.maxValue = _maxAmount;
        _slider.value = _minAmount;
        _text.text = _minAmount.ToString();
    }
}