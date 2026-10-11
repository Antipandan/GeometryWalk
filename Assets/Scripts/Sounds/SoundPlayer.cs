using System;
using System.Collections;
using UnityEngine;

public class SoundPlayer : MonoBehaviour, IDelayedDestruction
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
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

    private void CheckReferences()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyObjects());
    }

    private void OnDisable()
    {
        StopCoroutine(DestroyObjects());
    }

    public IEnumerator DestroyObjects()
    {
        // in case users somehow changes the time during coroutine is running
        float delay = DestructionDelay;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}