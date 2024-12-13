using UnityEngine;
using TMPro;
using System.Collections;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private bool _isCoroutineWork = false;
    private float _time = 0;
   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SwitchCoroutineState();
    }

    private void SwitchCoroutineState()
    {     
        if (_isCoroutineWork)
        {
            _isCoroutineWork = false;
        }
        else
        {
            _isCoroutineWork = true;
            StartCoroutine(CountSteps());
        }
    }

    private IEnumerator CountSteps()
    {
        float delayTime = 0.5f;     
        WaitForSeconds delay = new(delayTime);

        while (_isCoroutineWork)
        {
            _text.text = (++_time).ToString();

            yield return delay;
        }
    }
}