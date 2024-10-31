using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signal : MonoBehaviour
{
    [SerializeField] private float _rateOfChange;

    private AudioSource _audioSource;

    private float _currentVolume;
    private bool _isInside = false;

    private readonly float _minVolume = 0;
    private readonly float _maxVolume = 1;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    private void Start() => 
        _audioSource.volume = _minVolume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out _))
        {
            _isInside = true;

            if (_audioSource.isPlaying == false)
            {
                StartCoroutine(ChangeVolume());
                _audioSource.Play();
            }

            _currentVolume = _maxVolume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out _))
        {
            _isInside = false;
            _currentVolume = _minVolume;
        }
    }

    private IEnumerator ChangeVolume()
    {
        while (_audioSource.volume > 0f || _isInside == true)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _currentVolume, _rateOfChange * Time.deltaTime);
            yield return null;
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }
}