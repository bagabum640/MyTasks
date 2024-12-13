using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TownHall : MonoBehaviour
{
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private Gold _goldPrefab;

    private void OnMouseDown()
    {
        Instantiate(_goldPrefab, ResourseSpawnPosition(), Quaternion.identity);
    }

    private Vector2 ResourseSpawnPosition()
    {
        return new Vector2(Random.Range(_minPosition.x, _maxPosition.x),
                           Random.Range(_minPosition.y, _maxPosition.y));
    }
}