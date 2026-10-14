using UnityEngine;
using System;
using CustomUtility;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private EffectsImplementation explosionEffect;
    [SerializeField] private AudioClip deathSound;
    public event Action<Collision2D> OnCollision2D;

    private void Awake()
    {
        CheckReferences();
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