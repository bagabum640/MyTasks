using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Animator))]

public class SlimeAttack : MonoBehaviour
{
    [SerializeField] private Transform _raySource;
    [SerializeField] private SlimeSaliva _slimeSaliva;

    private readonly float _rayDistance = 3f;
    private readonly float _attackDelay = 0.5f;
    private readonly float _attackCooldown = 1.0f;

    private Player _target;
    private EnemyMovement _enemyMovement;
    private Animator _animator;
    
    private void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_raySource.position, transform.right, _rayDistance);

        Debug.DrawRay(_raySource.position, transform.right);
        if (hit && _target == null && hit.collider.TryGetComponent(out _target))
        {
            StartCoroutine(Atack());
        }
    }

    private IEnumerator Atack()
    {
        _enemyMovement.ProhibitMovement();
        _animator.SetTrigger(SlimeAnimator.Params.Attack);

        WaitForSeconds waitForSeconds = new(_attackDelay);    
        yield return waitForSeconds;
                
        SlimeSaliva saliva = Instantiate(_slimeSaliva, transform.position, transform.rotation);
        saliva.SetDirection(transform.right);        

        waitForSeconds = new (_attackCooldown);
        yield return waitForSeconds;

        _target = null;
        _enemyMovement.AllowMovement();
    }
}

public static class SlimeAnimator
{
    public static class Params
    {
        public static readonly int Attack = Animator.StringToHash("Atack");
    }
}