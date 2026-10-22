using System;
using CustomUtility;
using UnityEngine;

public abstract class PlayerMovementBase : MonoBehaviour, IUpdatableMovement
{
    [SerializeField] protected MovementBase movementBase;
    
    public virtual MovementBase Movement
    {
        get => movementBase;
    }
    
    // just ensuring this interface is attached to a MonoBehaviour
    public virtual GameObject GameObject
    {
        get => gameObject;
    }

    protected virtual void Awake()
    {
        CheckReferences();
    }

    protected virtual void OnEnable()
    {
        MovementBase newPlayerMovement = GetComponent<FootPlayerMovement>();
        movementBase = newPlayerMovement;
    }

    protected virtual void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref movementBase, gameObject, nameof(movementBase));
    }

    public virtual void UpdatePlayerMovementScheme<TMovementType, TMovementTypeSO>(TMovementTypeSO movementData) where TMovementType : PlayerMovement<TMovementTypeSO> where TMovementTypeSO : PlayerMovementSO
    {
        if (typeof(TMovementType) == movementBase.GetType()) return;
        TMovementType newMovement = gameObject.AddComponent<TMovementType>();
        newMovement.PlayerMovementData = movementData;
        Destroy(movementBase);
        movementBase = newMovement;
    }
}