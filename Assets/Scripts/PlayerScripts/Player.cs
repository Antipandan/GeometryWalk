using UnityEngine;
using System;
using CustomUtility;
using Unity.Mathematics;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private EffectsImplementation explosionEffect;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private MovementBase playerMovement; // problem
    private EventBinding<PlayerDeathEvent> deathEvent;
    
    private void Awake()
    {
        CheckReferences();
    }

    private void OnEnable()
    {
        deathEvent = new EventBinding<PlayerDeathEvent>(KillPlayer);
        EventBus<PlayerDeathEvent>.Register(deathEvent);
        MovementBase newPlayerMovement = GetComponent<FootPlayerMovement>();
        playerMovement = newPlayerMovement;
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

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref explosionEffect, gameObject, nameof(explosionEffect));
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

    private void PlayDeathSound()
    {
        if (deathSound != null) SoundPlayer.CreateInstance().PlaySound(deathSound);
    }

    public void UpdatePlayerMovementScheme<TMovementType, TMovementTypeSO>(TMovementTypeSO movementData) where TMovementType : PlayerMovement<TMovementTypeSO>
        where TMovementTypeSO : PlayerMovementSO
    {
        TMovementType newMovement = gameObject.AddComponent<TMovementType>();
        newMovement.PlayerMovementData = movementData;
        Destroy(playerMovement);
        playerMovement = newMovement;
    }
}