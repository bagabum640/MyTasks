using UnityEngine;

public class Heart : Item
{
    [SerializeField] private int _amountHealth = 1;

    protected override bool TryReactCollecting(Player player) 
    {
        return player.Health.TryCollectHeart(_amountHealth);
    }
}
