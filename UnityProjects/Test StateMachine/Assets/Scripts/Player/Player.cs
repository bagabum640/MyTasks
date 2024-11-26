using UnityEngine;

public class Player : MonoBehaviour,IVisitor
{
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IVisitable>(out IVisitable item))
        {
            item.Accept(this);
        }
    }

    public void Visit(HealthKit healthKit)
    {
        Debug.Log("HealthKit");
        _playerHealth.Restore(healthKit.HealthCount);
        Destroy(healthKit.gameObject);
    }

    public void Visit(Coin coin)
    {
        Debug.Log("Coin");
    }
}