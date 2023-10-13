using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class StepSound : MonoBehaviour
{
    [SerializeField] private AudioClip _stepSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _stepSound;
    }

    public void PlayStepSound()
    {
        _audioSource.PlayOneShot(_stepSound);
    }       
}
