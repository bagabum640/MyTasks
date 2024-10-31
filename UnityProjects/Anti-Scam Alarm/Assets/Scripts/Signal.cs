using UnityEngine;

public class Signal : MonoBehaviour
{
    [SerializeField] private float _playbackSpeed;

    private AudioSource _audioSource;

    private float _currentVolume;
    private bool _isPlaying = false;

    private readonly float _minVolume = 0;
    private readonly float _maxVolume = 1;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    private void Start()
    {
        _currentVolume = _minVolume;
        _audioSource.volume = _currentVolume;
    }

    private void Update()
    {
        if (_isPlaying == false)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _minVolume, _playbackSpeed * Time.deltaTime);
            _audioSource.volume = _currentVolume;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out _))
        {
            _isPlaying = true;
            _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _playbackSpeed * Time.deltaTime);
            _audioSource.volume = _currentVolume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out _))
            _isPlaying = false;
    }
}