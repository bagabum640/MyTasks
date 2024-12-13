using UnityEngine;
using UnityEngine.UI;

public class ResoursePanelController : MonoBehaviour
{
    private readonly int _maxWoodAmount = 100;
    private readonly int _maxFoodAmount = 100;

    [SerializeField] private GoldPanel _goldPanel;
    [SerializeField] private Slider _woodSlider;
    [SerializeField] private Slider _foodSlider;
    
    [SerializeField] private Text _woodText;
    [SerializeField] private Text _foodText;
}