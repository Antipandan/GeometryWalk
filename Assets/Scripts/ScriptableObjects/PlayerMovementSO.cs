using UnityEngine;

public abstract class PlayerMovementSO : ScriptableObject
{
    [SerializeField] protected float movementSpeed = GameConstants.defaultMovementSpeed;
    [SerializeField] protected float upwardsForce = GameConstants.defaultUpwardsForce;

    public float MovementSpeed
    {
        get => movementSpeed;
    }

    public float UpwardsForce
    {
        get => upwardsForce;
    }
}