using UnityEngine;

public class MusicSettings : ScriptableObject
{
    [SerializeField] [Range(0f, 1f)] private float volume = 1f;
    [SerializeField] [Range(-3, 3f)] private float pitch = 1f;
    [SerializeField] private bool playOnAwake = false;
    [SerializeField] private bool loop = true;

    public float Volume
    {
        get => volume;
        set => volume = Mathf.Clamp(value, 0f, 1f);
    }

    public float Pitch
    {
        get => pitch;
        set => pitch = Mathf.Clamp(value, -3f, 3f);
    }

    public bool PlayOnAwake
    {
        get => playOnAwake;
        set => playOnAwake = value;
    }

    public bool Loop
    {
        get => loop;
        set => loop = value;
    }

    public void ApplyToAudioSource(AudioSource source)
    {
        ApplyVolume(source);
        ApplyPitch(source);
        ApplyLoop(source);
        ApplyPlayOnAwake(source);
    }
    
    public void ApplyVolume(AudioSource source)
    {
        source.volume = volume;
    }

    public void ApplyPitch(AudioSource source)
    {
        source.pitch = pitch;
    }

    public void ApplyLoop(AudioSource source)
    {
        source.loop = loop;
    }

    public void ApplyPlayOnAwake(AudioSource source)
    {
        source.playOnAwake = playOnAwake;
    }
        
}