using UnityEngine;
using System;
using System.Collections.Generic;
using CustomUtility;
using JetBrains.Annotations;
using Unity.VisualScripting;

public abstract class ChangePlayerMovementPortal<TMovementType, TMovementTypeSO> : MonoBehaviour
    where TMovementType : PlayerMovement<TMovementTypeSO> where TMovementTypeSO : PlayerMovementSO
{
    // just making sure this exists
    [SerializeField] protected Collider2D entrenceZone;
    [SerializeField] protected TMovementTypeSO playerMovement;
    // hotfix for when we are swapping components in the portal >:(
    private bool hasBeenTriggered = false;
    
    protected Collider2D entrenceZoneCollider
    {
        get => entrenceZone;
    }

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
        if (!hasBeenTriggered && IsValidTarget(collision, out var player))
        {
            hasBeenTriggered = true;
            AddNewMovementComponentToPlayer(player);
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

    protected virtual void AddNewMovementComponentToPlayer(Player player)
    {
        player.UpdatePlayerMovementScheme<TMovementType, TMovementTypeSO>(playerMovement);
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