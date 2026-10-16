using UnityEngine;
using System;
using CustomUtility;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private EffectsImplementation explosionEffect;
    [SerializeField] private AudioClip deathSound;
    public event Action<Collision2D> OnCollision2D;
    private EventBinding<PlayerDeathEvent> deathEvent;
    private void Awake()
    {
        CheckReferences();
    }

    private void OnEnable()
    {
        deathEvent = new EventBinding<PlayerDeathEvent>(OnPlayerDied);
        EventBus<PlayerDeathEvent>.Register(deathEvent);
    }

    private void OnDisable()
    {
        EventBus<PlayerDeathEvent>.DeRegister(deathEvent);
    }

    private static void OnPlayerDied()
    {
        Debug.Log($"player dead");
    }

    private void OnCollisionEnter2D(Collision2D other)
    { 
        PublishOnCollisionEnter2D(other);
        if (damageMask.HasLayer(other.gameObject.layer))
        {
            KillPlayer();    
        }
    }

    private void PublishOnCollisionEnter2D(Collision2D other)
    {
        OnCollision2D?.Invoke(other);
    }
    
    private void KillPlayer()
    {
        PlayDeathSound();
        SpawnExplosionEffect();
        EventBus<PlayerDeathEvent>.Raise();
        Destroy(gameObject);
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref explosionEffect, gameObject, nameof(explosionEffect));
        ReferenceValidator.CheckObjectForNull(ref deathSound, gameObject, nameof(deathSound));
    }

    private void SpawnExplosionEffect()
    {
        if (explosionEffect != null)
        {
            IDelayedDestruction.CreateInstance(explosionEffect).ChangePosition(transform.position);
        }
    }

    private void PlayDeathSound()
    {
        if (deathSound != null) SoundPlayer.CreateInstance().PlaySound(deathSound);
    }
}