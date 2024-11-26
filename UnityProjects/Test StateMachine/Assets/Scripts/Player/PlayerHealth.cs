using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 10;

    public void Restore(int health)
    {
        Debug.Log("Восстановлено здоровье:" + health);     
    }
}