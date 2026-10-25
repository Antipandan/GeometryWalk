using UnityEngine;
using System;
using System.Diagnostics;
using CustomUtility;
using Unity.Mathematics;

public class Player : PlayerMovementBase
{
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private EffectsImplementation explosionEffect;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private ColliderHandler colliderHandler;
    // PlayerMovement<PlayerMovementSO> gamla PlayerMovement<PlayerMovementFootSO> passar ej?
    private EventBinding<PlayerDeathEvent> deathEvent;

    protected override void OnEnable()
    {
        deathEvent = new EventBinding<PlayerDeathEvent>(KillPlayer);
        EventBus<PlayerDeathEvent>.Register(deathEvent);
        base.OnEnable();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (damageMask.HasLayer(other.gameObject.layer))
        {
            EventBus<PlayerDeathEvent>.Raise();
        }
    }

    private void OnDestroy()
    {
        EventBus<PlayerDeathEvent>.DeRegister(deathEvent);
    }
    
    private void KillPlayer()
    {
        PlayDeathSound();
        SpawnExplosionEffect();
        Destroy(gameObject);
    }

    protected override void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref explosionEffect, gameObject, nameof(explosionEffect));
        ReferenceValidator.CheckComponentForNull(ref colliderHandler, gameObject, nameof(colliderHandler));
        ReferenceValidator.CheckObjectForNull(ref deathSound, gameObject, nameof(deathSound));
    }

    private void SpawnExplosionEffect()
    {
        Vector3 position = gameObject.transform.position;
        if (explosionEffect != null)
        {
            IDelayedDestruction.CreateInstance(explosionEffect, position);
        }
    }

    public void DeactivateAllComponents()
    {
        GetComponent<MovementBase>().enabled = false;
        AngleLerper lerper = GetComponent<AngleLerper>();
        if (lerper != null) lerper.enabled = false;
        if (colliderHandler != null) colliderHandler.enabled = false;
        if (gameObject.TryGetComponent<BoxCollider2D>(out var boxCollider2D)) boxCollider2D.enabled = false;
        enabled = false;
    }

    public void MutateMovement<TMovement, TMovementSO>(TMovement desiredMovementScript, TMovementSO movementData) where TMovementSO : PlayerMovementSO
        where TMovement : PlayerMovement<TMovementSO>
    {
        if (desiredMovementScript.GetType() == typeof(FootPlayerMovement)) MutateFootMovement();
        else if (desiredMovementScript.GetType() == typeof(ShipPlayerMovement)) MutateShipMovement();
    }

    private void MutateFootMovement()
    {
        
    }

    private void MutateShipMovement()
    {
        
    }

    private void PlayDeathSound()
    {
        if (deathSound != null) SoundPlayer.CreateInstance().PlaySound(deathSound);
    }
    
}