using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class AlarmSpeaker : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    readonly private float _minVolume = 0f;
    readonly private float _maxVolume = 1f;

    private AudioSource _audioSource;    
    private Coroutine _coroutine;

    public void ChangeTargetVolume(float duration, bool isEnter)
    {
        float targetVolume;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (isEnter)
        {
            _audioSource.Play();
            targetVolume = _maxVolume;
            _coroutine = StartCoroutine(ChangeVolume(duration, targetVolume));
        }
        else
        {
            targetVolume = _minVolume;
            _coroutine = StartCoroutine(ChangeVolume(duration, targetVolume));
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.volume = _minVolume;

        if (_audioSource.loop == false)
        {
            _audioSource.loop = true;
        }
    }

    private IEnumerator ChangeVolume(float duration, float targetVolume)
    {
        if (duration <= 0)
            duration = 1;

        float speed = _maxVolume / duration;
        WaitForSeconds waitForSeconds = new(1f);

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, speed);

            yield return waitForSeconds;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}
