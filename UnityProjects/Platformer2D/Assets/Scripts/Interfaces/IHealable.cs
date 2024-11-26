using System;

public interface IHealable
{
    public float _healCount {  get; set; }

    public static event Action<float> IsHealing;
}