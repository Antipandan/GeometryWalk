using System;
using CustomUtility;
using UnityEngine;
using UnityEngine.Events;

public sealed class ColliderHandler : MonoBehaviour
{
    [SerializeField] private GameObject attachedObject;
    [SerializeField] private UnityEvent onCollision;
    [SerializeField] private UnityEvent onCollisionExit;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private LayerMask damageMask;

    private void Awake()
    {
        CheckReferences();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubScribeEvents();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PublishOnCollisionEnter2D(other);
        if (damageMask.HasLayer(other.gameObject.layer) || CheckCollisionDifferential(other))
        {
            PublishOnPlayerKilled();    
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        PublishOnCollisionExit2D(other);
    }

    private bool CheckCollisionDifferential(Collision2D collision)
    {
        Transform other = collision.gameObject.transform;
        if (other == null) return false;
        Vector2 dir = attachedObject.transform.position - other.position;
        float dotProduct = Vector2.Dot(Vector2.up, dir);
        float playerColliderSizeY = playerCollider.bounds.extents.y;
        float offsetDotProduct = dotProduct - playerColliderSizeY / 2f - other.GetComponent<Collider2D>().bounds.extents.y / 2f;
        return Math.Sign(offsetDotProduct) == -1;
    }

    private void PublishOnCollisionEnter2D(Collision2D other)
    {
        onCollision?.Invoke();
        EventBus<Collision2D>.Raise(other);
    }

    private void PublishOnCollisionExit2D(Collision2D other)
    {
        onCollisionExit?.Invoke();
        EventBus<Collision2DEnd>.Raise(new Collision2DEnd(other));
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref playerCollider, gameObject, nameof(playerCollider));
        if (attachedObject == null) attachedObject = gameObject;
    }

    private void SubscribeEvents()
    {
        
    }

    private void UnSubScribeEvents()
    {
        
    }

    private void PublishOnPlayerKilled()
    {
        EventBus<PlayerDeathEvent>.Raise();
    }
    
}