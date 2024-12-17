using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseBorderDraw : MonoBehaviour
{
    //[SerializeField] private Image _border;
    //[SerializeField] private Camera _camera;

    //private List<Unit> _unitsInBorder = new();
    //private Unit[] _units;
    //private Ray _ray;
    //private RaycastHit _hit;
    //private Vector2 _screenPosition;
    //private Vector2 _frameStart;
    //private Vector2 _frameEnd;
    //private Vector2 _minVector;
    //private Vector2 _maxVector;
    //private Rect _rect;

    //public  Vector2 BorderSize => _maxVector - _minVector;

    //private void Awake()
    //{
    //    _border.gameObject.SetActive(false);
    //}

    //void Update()
    //{
    //    DrawBorder();     
    //}

    //private void SelectUnit()
    //{
    //    _ray = _camera.ScreenPointToRay(Input.mousePosition);
    //    Debug.DrawRay(_ray.origin, _ray.direction * 10f, Color.red);

    //    if (Physics.Raycast(_ray, out _hit))
    //    {
    //        if(_hit.collider.TryGetComponent<Unit>(out Unit unit))
    //        {
    //            unit.Select();
    //        }
    //    }
    //}

    //private void DrawBorder()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //SelectUnit();
    //        _border.gameObject.SetActive(true);
    //        _frameStart = Input.mousePosition;
    //    }
    //    if (Input.GetMouseButton(0))
    //    {
    //        _frameEnd = Input.mousePosition;

    //        _minVector = Vector2.Min(_frameStart, _frameEnd);
    //        _maxVector = Vector2.Max(_frameStart, _frameEnd);

    //        _border.rectTransform.anchoredPosition = _minVector;
    //        _border.rectTransform.sizeDelta = BorderSize;

    //        _rect = new Rect(_minVector, _maxVector);

    //        _units = FindObjectsOfType<Unit>();

    //        foreach (Unit unit in _units)
    //        {
    //            _screenPosition = _camera.WorldToScreenPoint(unit.transform.position);

    //            if (_rect.Contains(_screenPosition))
    //            {
    //                _unitsInBorder.Add(unit);
    //                unit.Select();
    //            }
    //            else
    //            {
    //                unit.Unselect();
    //            }
    //        }
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        _border.gameObject.SetActive(false);
    //    }
    //}
}