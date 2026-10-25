using System;
using System.Collections;
using CustomUtility;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour, IDelayedDestruction
{
    [SerializeField] private AudioSource audioSource;
    private float destructionDelay;

    public float DestructionDelay
    {
        get => destructionDelay;
        set => destructionDelay = value;
    }

    public GameObject @GameObject
    {
        get => gameObject;
    }

    private void Awake()
    {
        CheckReferences();
    }

    private void OnDisable()
    {
        StopCoroutine(DestroyObjects());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public static SoundPlayer CreateInstance()
    {
        SoundPlayer soundPlayer = new GameObject(nameof(SoundPlayer)).AddComponent<SoundPlayer>();
        return soundPlayer;
    }

    public static SoundPlayer CreateInstance(AudioClip audioClip)
    {
        SoundPlayer player = CreateInstance();
        player.audioSource.clip = audioClip;
        return player;
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;
        audioSource.clip = clip;
        audioSource.Play();
        DestructionDelay = clip.length;
        StartCoroutine(DestroyObjects());
    }

    private void PlaySound()
    {
        audioSource.Play();
        DestructionDelay = audioSource.clip.length;
        StartCoroutine(DestroyObjects());
    }
    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref audioSource, gameObject, nameof(audioSource));
    }

    public IEnumerator DestroyObjects()
    {
        // in case users somehow changes the time during coroutine is running
        float delay = DestructionDelay;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}