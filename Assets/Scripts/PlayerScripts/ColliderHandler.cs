using System;
using CustomUtility;
using UnityEngine;
using UnityEngine.Events;

public class ColliderHandler : MonoBehaviour
{
    [SerializeField] protected UnityEvent onCollision;
    [SerializeField] protected Collider2D playerCollider;
    [SerializeField] protected LayerMask damageMask;
    protected void Awake()
    {
        CheckReferences();
    }

    protected virtual void OnEnable()
    {
        SubscribeEvents();
    }

    protected virtual void OnDisable()
    {
        UnSubScribeEvents();
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        PublishOnCollisionEnter2D(other);
        if (damageMask.HasLayer(other.gameObject.layer) || CheckCollisionDifferential(other))
        {
            PublishOnPlayerKilled();    
        }
    }

    protected virtual bool CheckCollisionDifferential(Collision2D collision)
    {
        Transform other = collision.gameObject.transform;
        if (other == null) return false;
        Vector2 dir = gameObject.transform.position - other.position;
        float dotProduct = Vector2.Dot(Vector2.up, dir);
        float playerColliderSizeX = playerCollider.bounds.extents.x;
        float offsetDotProduct = dotProduct - playerColliderSizeX / 2f;
        return Math.Sign(offsetDotProduct) == -1;
    }

    protected virtual void PublishOnCollisionEnter2D(Collision2D other)
    {
        onCollision?.Invoke();
        EventBus<Collision2D>.Raise(other);
    }

    protected virtual void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref playerCollider, gameObject, nameof(playerCollider));
    }

    protected virtual void SubscribeEvents()
    {
        
    }

    protected virtual void UnSubScribeEvents()
    {
        
    }

    protected virtual void PublishOnPlayerKilled()
    {
        EventBus<PlayerDeathEvent>.Raise();
    }
    
}