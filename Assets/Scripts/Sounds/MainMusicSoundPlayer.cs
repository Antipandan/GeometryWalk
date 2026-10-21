using System;
using CustomUtility;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public sealed class MainMusicSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MusicSettings mainMusicSettings;
    private static MainMusicSoundPlayer instance;
    [CanBeNull]
    public static MainMusicSoundPlayer Instance
    {
        get
        {
            if (instance != null) return instance;
            MainMusicSoundPlayer newInstance = CreateInstance();
            instance = newInstance;
            return newInstance;
        }
    }

    public float Volume
    {
        get => audioSource.volume;
    }
    
    public AudioClip MusicAudioClip
    {
        get => audioSource.clip;
        set
        {
            if (audioSource == null)
            {
                return;
            }
            audioSource.clip = value;   
        }
    }

    private void Awake()
    {
        CheckReferences();
        CheckSingleton();
        if (mainMusicSettings != null) ChangeVolume(mainMusicSettings.Volume);
    }
    

    private void CheckSingleton()
    {
        if (instance == null || instance == this)
        {
            instance = this;
        }
        else
        {
            Logging.LogSingletonError(nameof(MainMusicSoundPlayer), ErrorSeverity.Error);
            Destroy(this);
        }
    }

    public static MainMusicSoundPlayer CreateInstance()
    {
        return instance == null ? null : new GameObject("MainSoundPlayer").AddComponent<MainMusicSoundPlayer>();
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref audioSource, gameObject, nameof(audioSource));
        ReferenceValidator.CheckUnityObjectForNull(ref mainMusicSettings, nameof(mainMusicSettings));
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

    public void ChangeVolume(float volume = 1f)
    {
        audioSource.volume = ClampVolume(volume);
    }

    public AudioSource SetVolume(float volume = 1f)
    {
        ChangeVolume(volume);
        return audioSource;
    }
    
    public AudioSource SetPitch(float pitch)
    {
        audioSource.pitch = pitch;
        return audioSource;
    }
}