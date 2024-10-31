using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _target;
    [SerializeField] private float _delay;

    private readonly bool _isWork = true;

    private void Start() =>  
        StartCoroutine(Shoot());   

    private IEnumerator Shoot()
    {
        WaitForSeconds waitForSeconds = new (_delay);

        while (_isWork)
        {
            Instantiate(_bullet, transform.position, transform.rotation).SetDirection((_target.position - transform.position).normalized);

            yield return waitForSeconds;
        }
    }
}