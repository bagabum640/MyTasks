using UnityEngine;

public class ResourcePoint : MonoBehaviour
{
    protected Resource _resourcePrefab;

    public Resource Resource { get; private set; }

    public Resource GetResource(Vector3 handPosition)
    {
        Resource = Instantiate(_resourcePrefab, handPosition, Quaternion.identity);
        Resource.GetAmount();

        return Resource;
    }
}