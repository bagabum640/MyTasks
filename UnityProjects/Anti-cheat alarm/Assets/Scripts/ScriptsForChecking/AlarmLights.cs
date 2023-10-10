using UnityEngine;

public class AlarmLights : MonoBehaviour
{
    [SerializeField] private Color _alarmColor;
    
    private Light[] _lights;
    private Color _startLightColor;
    private Color _targetLightColor;
    private float _runTime;

    public void ChangeColor(float runTime)
    {        
        _runTime = runTime;

        if (_targetLightColor == _startLightColor)
            _targetLightColor = _alarmColor;
        else
            _targetLightColor = _startLightColor;
    }

    private void Start()
    {        
        _lights = GetComponentsInChildren<Light>();
        _startLightColor = _lights[0].color;        
    }

    private void Update()
    {
        float runningTime =+ Time.deltaTime;

        foreach (Light light in _lights)
            light.color = new Color(r: Mathf.MoveTowards(light.color.r, _targetLightColor.r, runningTime/ _runTime), g: Mathf.MoveTowards(light.color.g, _targetLightColor.g, runningTime / _runTime), b: Mathf.MoveTowards(light.color.b, _targetLightColor.b, runningTime / _runTime));
                
        if (_lights[0].color == _targetLightColor)        
            enabled = false;  
    }
}
