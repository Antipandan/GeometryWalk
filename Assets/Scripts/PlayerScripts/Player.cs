using UnityEngine;
using System;
using CustomUtility;
using Unity.Mathematics;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private EffectsImplementation explosionEffect;
    [SerializeField] private AudioClip deathSound;
    public event Action<Collision2D> OnCollision2D;
    private EventBinding<PlayerDeathEvent> deathEvent;
    private float playerColliderSize;
    private void Awake()
    {
        CheckReferences();
    }

    private void OnEnable()
    {
        deathEvent = new EventBinding<PlayerDeathEvent>(OnPlayerDied);
        EventBus<PlayerDeathEvent>.Register(deathEvent);
        playerColliderSize = GetComponent<BoxCollider2D>().size.x;
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
        CheckCollisionDifferential(other);
        PublishOnCollisionEnter2D(other);
        if (damageMask.HasLayer(other.gameObject.layer) || CheckCollisionDifferential(other))
        {
            KillPlayer();    
        }
    }

    private bool CheckCollisionDifferential(Collision2D collision)
    {
        Transform other = collision.gameObject.transform;
        if (other == null) return false;
        Vector2 dir = gameObject.transform.position - other.position;
        float dotProduct = Vector2.Dot(Vector2.up, dir);
        float offsetDotProduct = dotProduct - playerColliderSize / 2f;
        return Math.Sign(offsetDotProduct) == -1;
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
        Vector3 position = gameObject.transform.position;
        Debug.Log($"position: {position}");
        Debug.Log($"gameObject position: {gameObject.transform.position}");
        if (explosionEffect != null)
        {
            IDelayedDestruction.CreateInstance(explosionEffect, position);
        }
    }

    private void PlayDeathSound()
    {
        if (deathSound != null) SoundPlayer.CreateInstance().PlaySound(deathSound);
    }
}