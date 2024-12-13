using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class OpeningDoor : MonoBehaviour, IInteractable
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
        _isOpen = !_isOpen;
        _animator.SetBool(SwitchDoorAnimator.Params.IsOpen, _isOpen);
        _audioSource.PlayOneShot(_audioClip);        
    }
}

public static class SwitchDoorAnimator
{
    public static class Params
    {
        public static readonly int IsOpen = Animator.StringToHash("IsOpen");
    }
}