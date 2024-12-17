public class FoodPanel : Panel
{
    public void UpdateFoodPanel(int amount)
    {
        _slider.value += amount;
        _text.text = _slider.value.ToString();
    }
}