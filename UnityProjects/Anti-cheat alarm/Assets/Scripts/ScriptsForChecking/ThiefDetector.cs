using UnityEngine;

[RequireComponent(typeof(AlarmLights))]
[RequireComponent(typeof(AlarmSpeaker))]

public class ThiefDetector : MonoBehaviour
{
    [SerializeField] private float _forceGrowthTime;

    private AlarmSpeaker _alarmSpeaker;
    private AlarmLights _alarmLights;

    private void Start()
    {
        _alarmSpeaker = GetComponent<AlarmSpeaker>();
        _alarmSpeaker.enabled = false;
        _alarmLights = GetComponent<AlarmLights>();
        _alarmLights.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            SwitchSpeaker();
            SwitchLightsColor();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            SwitchSpeaker();
            SwitchLightsColor();
        }
    }

    private void SwitchSpeaker()
    {
        _alarmSpeaker.enabled = true;
        _alarmSpeaker.ChangeTargetVolume(_forceGrowthTime);
    }

    private void SwitchLightsColor()
    {
        _alarmLights.enabled = true;
        _alarmLights.ChangeColor(_forceGrowthTime);
    }
}
