using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _raySource;
    [SerializeField] private Transform _rayDirection;
    [SerializeField] private TextMeshProUGUI _interactionText;
    [SerializeField] private GameObject _interactionUI;

    private const KeyCode Key = KeyCode.E;    
    private bool _isHit;
    private float _rayDistance = 3f;
    
    private void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        Ray ray = new Ray(_raySource.position, _rayDirection.forward);
        _isHit = false;

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                _isHit = true;
                _interactionText.text = interactable.GetDescription();

                if (Input.GetKeyDown(Key))
                    interactable.Interact();
            }                     
        }

        _interactionUI.SetActive(_isHit);
    }
}
