using System;
using CustomUtility;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public sealed class MainMusicSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private static MainMusicSoundPlayer instance;
    [CanBeNull]
    public static MainMusicSoundPlayer Instance
    {
        get => instance;
    }

    public AudioClip MusicAudioClip
    {
        
        get => audioSource.clip;
        set
        {
            if (audioSource == null) return;
            audioSource.clip = value;   
        }
    }

    private void Awake()
    {
        CheckReferences();
        CheckSingleton();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void CheckSingleton()
    {
        if (instance == null) instance = this;
        else
        {
            Logging.LogSingletonError(nameof(MainMusicSoundPlayer), ErrorSeverity.Error);
            Destroy(this);
        }
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref audioSource, gameObject, nameof(audioSource));
    }

    private void SetupAudioSource()
    {
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.volume = 1f;
        audioSource.pitch = 1f;
    }

    private static float ClampVolume(float volume)
    {
        return GameUtilities.ClampVolume(volume);
    }

    public AudioSource SetVolume(float volume = 1f)
    {
        audioSource.volume = volume;
        return audioSource;
    }
    
    public AudioSource SetPitch(float pitch)
    {
        audioSource.pitch = pitch;
        return audioSource;
    }
}