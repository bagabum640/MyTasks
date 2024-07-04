using UnityEngine;
using TMPro;
using System.Collections;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private bool _isCoroutineWork = false;
   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SwitchCoroutineState();
    }

    private void SwitchCoroutineState()
    {
        _isCoroutineWork = !_isCoroutineWork;

        if (_isCoroutineWork == true)           
            StartCoroutine(CountSteps());
    }

    private IEnumerator CountSteps()
    {
        float delayTime = 0.5f;
        float step = 1f;

        WaitForSeconds delay = new(delayTime);

        while (_isCoroutineWork)
        {
            _text.text = (float.Parse(_text.text) + step).ToString();

            yield return delay;
        }
    }
}