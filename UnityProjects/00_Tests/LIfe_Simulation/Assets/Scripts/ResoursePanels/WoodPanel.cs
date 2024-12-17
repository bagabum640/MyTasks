public class WoodPanel : Panel
{
    public void UpdateWoodPanel(int amount)
    {
        _slider.value += amount;
        _text.text = _slider.value.ToString();
    }
}