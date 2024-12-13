using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource),
                  typeof(Observer))]
public class SoundAlarm : MonoBehaviour
{
    [SerializeField] private float _rateOfChange;

    private AudioSource _audioSource;
    private Observer _observer;
    private float _currentVolume;

    private readonly float _minVolume = 0;
    private readonly float _maxVolume = 1;
    private readonly float _delay = 0.1f;

    private void Awake()
    {
        _observer = GetComponent<Observer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() =>
        _observer.IsTrigger += SetSignal;

    private void Start() =>
        _audioSource.volume = _minVolume;

    private IEnumerator PlaySignal(bool isInside)
    {
        WaitForSeconds waitForSeconds = new(_delay);

        _audioSource.Play();

        while (_audioSource.volume > 0 || isInside)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _currentVolume, _rateOfChange);
            yield return waitForSeconds;
        }

        _audioSource.Stop();
    }

    private void OnDisable() =>
        _observer.IsTrigger -= SetSignal;

    private void SetSignal(bool isInside)
    {
        if (isInside)
        {
            if (!_audioSource.isPlaying)
                StartCoroutine(PlaySignal(isInside));

            _currentVolume = _maxVolume;
        }
        else
        {
            _currentVolume = _minVolume;
        }
    }
}