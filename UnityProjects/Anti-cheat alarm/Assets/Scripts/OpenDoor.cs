using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class OpenDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private AudioClip _audioClip;

    private Animator _animator;
    private bool _isOpen = false;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = _pivot.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
    }

    public string GetDescription()
    {
        return gameObject.name;
    }

    public void Interact()
    {
        if (_isOpen == false)
        {
            _animator.SetBool("IsOpen", true);
        }
        else
        {
            _animator.SetBool("IsOpen", false);
        }

        _audioSource.PlayOneShot(_audioClip);
        _isOpen = !_isOpen;
    }
}
