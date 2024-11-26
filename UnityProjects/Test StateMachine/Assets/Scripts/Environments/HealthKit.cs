using UnityEngine;

public class HealthKit : MonoBehaviour,IVisitable
{
    public int HealthCount { get; private set; } = 3;

    public void Accept(IVisitor visitor)
    {       
        visitor.Visit(this);
    }
}