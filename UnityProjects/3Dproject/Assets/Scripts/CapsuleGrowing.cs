using UnityEngine;

public class CapsuleGrowing : MonoBehaviour
{
    [SerializeField] private float _growingSpeed;

    private void Update() => transform.localScale += _growingSpeed * Time.deltaTime * Vector3.one;
}