using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _heartTemplate;

    private readonly Stack<Image> _hearts = new();
    private readonly float _heartStartPosition = 5;

    private float _heartPosition;
       
    public void AddHeart(int quntity)
    {
        for (int i = 0; i < quntity; i++)
        {
            _heartPosition = _hearts.Count > 0 ? _hearts.Peek().rectTransform.anchoredPosition.x + _heartTemplate.preferredWidth / 2 : _heartStartPosition;

            Image heart = Instantiate(_heartTemplate, transform);
            heart.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _heartPosition, _heartTemplate.preferredWidth);
            _hearts.Push(heart);
        }
    }

    public void RemoveHeart(int quntity)
    {
        for (int i = 0; i < quntity; i++)        
            if (_hearts.Count > 0)
                Destroy(_hearts.Pop().gameObject);        
    }
}
