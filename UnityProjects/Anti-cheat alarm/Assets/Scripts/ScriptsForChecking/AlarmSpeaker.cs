using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSpeaker : MonoBehaviour
{    
    [SerializeField] private AudioClip _audioClip;

    readonly private float _minVolume = 0f;
    readonly private float _maxVolume = 1f;

    private AudioSource _audioSource;
    private float _targetVolume;
    private float _runTime;

    public void ChangeTargetVolume(float runTime)
    {
        _runTime = runTime;
        
        if (_targetVolume == _minVolume)
            _targetVolume = _maxVolume;
        else
            _targetVolume = _minVolume;         
    }
        
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.volume = _minVolume;        
    }

    private void Update()
    {
        float runningTime = +Time.deltaTime;

        _audioSource.Play();
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, runningTime / _runTime);        

        if (_audioSource.volume <= 0)
            enabled = false;
    }
}
