using UnityEngine;

[RequireComponent(typeof(AlarmSpeaker))]

public class ThiefDetector : MonoBehaviour
{
    [SerializeField] private float _duration = 5;

    private AlarmSpeaker _alarmSpeaker;

    private void Start()
    {
        _alarmSpeaker = GetComponent<AlarmSpeaker>(); 
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out _))
        {
            _alarmSpeaker.ChangeTargetVolume(_duration, true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out _))
        {
            _alarmSpeaker.ChangeTargetVolume(_duration, false);
        }
    }
}
