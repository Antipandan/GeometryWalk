using System;
using System.Collections;
using CustomUtility;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EffectsImplementation : MonoBehaviour, IDelayedDestruction
{
    [SerializeField] private float destructionDelay = 1f;
    [SerializeField] private ParticleSystem explosionEffect;
    public GameObject GameObject
    {
        get => gameObject;
    }

    public float DestructionDelay
    {
        get => destructionDelay;
        set => destructionDelay = value;
    }

    private void Awake()
    {
        CheckReferences();
    }
    
    private void OnEnable()
    {
        SetupValues();
        StartCoroutine(DestroyObjectDelayed());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref explosionEffect, gameObject, nameof(explosionEffect));
    }

    private void SetupValues()
    {
        if (explosionEffect != null) destructionDelay = explosionEffect.main.duration;
    }

    public IEnumerator DestroyObjectDelayed()
    {
        yield return new WaitForSeconds(destructionDelay);
        Destroy(gameObject);
    }

}