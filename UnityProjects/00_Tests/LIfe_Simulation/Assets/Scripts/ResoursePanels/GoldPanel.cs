using UnityEngine;
using UnityEngine.UI;

public class GoldPanel : MonoBehaviour
{
    private readonly int _maxGoldAmount = 100;
    private readonly int _minGoldAmount = 0;

    [SerializeField] private Slider _goldSlider;
    [SerializeField] private Text _goldText;

    private void Awake()
    {
        _goldSlider.maxValue = _maxGoldAmount;
        _goldSlider.value = _minGoldAmount;
        _goldText.text = _minGoldAmount.ToString();
    }

    public void UpdateMoneyView(int amount) 
    {
        _goldSlider.value += amount;
        _goldText.text = _goldSlider.value.ToString();
    }
}