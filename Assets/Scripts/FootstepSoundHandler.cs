using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FootstepSoundHandler : MonoBehaviour
{
    public AudioClip Step1;
    public float StepDelay = 0.5f;
    private float _timeSinceLastStep;

    [SerializeField] private AudioSource _audioSource;
    private FirstPersonController _firstPersonController;

    void Awake(){

    }
    void Start()
    {
        _firstPersonController = GetComponent<FirstPersonController>();
        // _audioSource = GetComponent<AudioSource>();
        _timeSinceLastStep = 0.0f;
    }

    void Update()
    {
        if (_firstPersonController != null && _audioSource != null)
        {
            _timeSinceLastStep += Time.deltaTime;

            if (!_audioSource.isPlaying && _firstPersonController.MovementInput != Vector2.zero && _firstPersonController.Grounded && _timeSinceLastStep >= StepDelay)
            {
                _audioSource.PlayOneShot(Step1);
                _timeSinceLastStep = 0.0f;
            }
        }
        else if (_firstPersonController == null)
        {
            Debug.Log("FootstepSoundHandler: FirstPersonController is null");
        }
        else if (_audioSource == null)
        {
            Debug.Log("FootstepSoundHandler: AudioSource is null");
        }
    }
}
