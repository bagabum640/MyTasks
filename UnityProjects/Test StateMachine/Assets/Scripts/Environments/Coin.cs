using UnityEngine;

public class Coin : MonoBehaviour, IVisitable
{
    public void Accept(IVisitor visitor)
    {        
        visitor.Visit(this);
    }
}