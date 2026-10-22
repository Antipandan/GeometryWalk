using UnityEngine;

public interface IUpdatableMovement
{
    public MovementBase Movement
    {
        get;
    }
    
    // just ensuring this interface is attached to a MonoBehaviour
    public GameObject GameObject
    {
        get;
    }

    public void UpdatePlayerMovementScheme<TMovementType, TMovementTypeSO>(TMovementTypeSO movementData)
        where TMovementType : PlayerMovement<TMovementTypeSO>
        where TMovementTypeSO : PlayerMovementSO;
}