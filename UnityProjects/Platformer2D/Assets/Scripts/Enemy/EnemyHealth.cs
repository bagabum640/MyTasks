using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyHealth : MonoBehaviour
{
    [field: SerializeField] public int Health { get; private set; }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            StartCoroutine(DestroyEnemy());
        }
        else
        {
            _animator.SetTrigger("Hurt");
        }
    }

    private IEnumerator DestroyEnemy()
    {
        float delay = 1.5f;

        GetComponent<Collider2D>().enabled = false;

        _animator.SetTrigger("Death");

        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}