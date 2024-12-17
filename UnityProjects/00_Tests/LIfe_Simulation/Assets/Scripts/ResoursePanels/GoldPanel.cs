public class GoldPanel : Panel
{
    public void UpdateGoldPanel(int amount)
    {
        _slider.value += amount;
        _text.text = _slider.value.ToString();
    }
}