using UnityEngine;
using System;
using System.Collections.Generic;
using CustomUtility;
using JetBrains.Annotations;
using Unity.VisualScripting;

public abstract class ChangePlayerMovementPortal<TMovementTypeSO> : MonoBehaviour where TMovementTypeSO : PlayerMovementSO
{
    // just making sure this exists
    [SerializeField] private Collider2D entrenceZone;

    protected virtual void Awake()
    {
        CheckReferences();
    }

    protected void OnEnable()
    {
        SetupValues();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsValidTarget(collision, out var player))
        {
            PlayerMovement<TMovementTypeSO> movementComponent = CreatePlayerMovement();
            player?.UpdatePlayerMovementScheme(movementComponent);
        }
    }

    protected virtual void SetupValues()
    {
        SetupEntrence();
    }

    private void SetupEntrence()
    {
        if (entrenceZone != null) entrenceZone.isTrigger = true;
    }
    
    protected PlayerMovement<TMovementTypeSO> CreatePlayerMovement()
    {
        return new Component().AddComponent<PlayerMovement<TMovementTypeSO>>();
    }
    
    protected virtual void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref entrenceZone, gameObject, nameof(entrenceZone));
    }
    
    protected virtual bool IsValidTarget(Collider2D collision, [CanBeNull] out Player player)
    {
        return collision.TryGetComponent<Player>(out player);
    }
}