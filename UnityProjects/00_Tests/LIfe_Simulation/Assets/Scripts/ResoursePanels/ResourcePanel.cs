using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] private GoldPanel _goldPanel;
    [SerializeField] private WoodPanel _woodPanel;
    [SerializeField] private FoodPanel _foodPanel;

    public void UpdatePanel(Resource resource)
    {
        if (resource as Golden)
        {
            _goldPanel.UpdateGoldPanel(resource.Amount);
        }
        else if (resource as Wood)
        {
            _woodPanel.UpdateWoodPanel(resource.Amount);
        }
        else if (resource as Food)
        {
            _foodPanel.UpdateFoodPanel(resource.Amount);
        }
    }

    public void SpendTextResources(int goldCost, int woodCost, int foodCost)
    {
        _goldPanel.UpdateGoldPanel(-goldCost);
        _woodPanel.UpdateWoodPanel(-woodCost);
        _foodPanel.UpdateFoodPanel(-foodCost);     
    }
}