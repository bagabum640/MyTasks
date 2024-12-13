using UnityEngine;
using DG.Tweening;

public abstract class Item : MonoBehaviour
{
    Tween _tween;

    private void Start()
    {
        DoAnimation();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Player>(out Player player) && TryReactCollecting(player))
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }

    protected abstract bool TryReactCollecting(Player player);

    protected virtual void DoAnimation() => _tween = transform.DOMoveY(transform.position.y + 1, 1).SetLoops(-1, LoopType.Yoyo);
}
