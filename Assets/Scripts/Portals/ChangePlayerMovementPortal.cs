using UnityEngine;
using System;
using System.Collections.Generic;
using CustomUtility;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.Events;

public abstract class ChangePlayerMovementPortal<TMovementType, TMovementTypeSO> : MonoBehaviour
    where TMovementType : PlayerMovement<TMovementTypeSO> where TMovementTypeSO : PlayerMovementSO
{
    // just making sure this exists
    [SerializeField] protected Collider2D entrenceZone;
    [SerializeField] protected TMovementTypeSO playerMovement;
    [SerializeField] [Tooltip("Add extra functionality if you want." +
                              " If anyone puts a game destroying function(s) here I'll mangle your spongebob ice cream")]
    protected UnityEvent onPlayerEnter;
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
            onPlayerEnter?.Invoke();
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