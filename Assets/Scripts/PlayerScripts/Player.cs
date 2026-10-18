using UnityEngine;
using System;
using CustomUtility;
using Unity.Mathematics;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private EffectsImplementation explosionEffect;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private PlayerMovement<PlayerMovementSO> playerMovement;
    private EventBinding<PlayerDeathEvent> deathEvent;
    
    private void Awake()
    {
        CheckReferences();
    }

    private void OnEnable()
    {
        deathEvent = new EventBinding<PlayerDeathEvent>(KillPlayer);
        EventBus<PlayerDeathEvent>.Register(deathEvent);
        var playerMovement = GetComponent<FootPlayerMovement>();
        
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

    public void UpdatePlayerMovementScheme<TMovementSO>() where TMovementSO : PlayerMovementSO
    {
        
    }

    // Talk is cheap. Show me the code.
    public void UpdatePlayerMovementScheme<TMovementSO>(PlayerMovement<TMovementSO> movementScript)
        where TMovementSO : PlayerMovementSO
    {
        
    }
}