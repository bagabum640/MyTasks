using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TownHall : MonoBehaviour
{
    [SerializeField] private Image _resoursePanel;

    private bool _isActive = false;

    private void Start()
    {
        _resoursePanel.gameObject.SetActive(_isActive);
        Debug.Log(_resoursePanel.GetType());
    }

    private void OnMouseDown()
    {
        if (_isActive == false)
        {
            _isActive = !_isActive;
            _resoursePanel.gameObject.SetActive(_isActive);
        }
        else
        {
            _isActive = !_isActive;
            _resoursePanel.gameObject.SetActive(_isActive);
        }
    }
}