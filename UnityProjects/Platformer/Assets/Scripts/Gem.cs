public class Gem : Item
{
    protected override bool TryReactCollecting(Player player)
    {        
        Events.SendGemCollected(gameObject.transform.parent);
        return true;
    }
}
