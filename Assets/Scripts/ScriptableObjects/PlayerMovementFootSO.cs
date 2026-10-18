using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/PlayerMovementFootSO", fileName = "PlayerMovementFootSO")]
public class PlayerMovementFootSO : PlayerMovementSO
{
    [SerializeField] private float rotationSpeed = GameConstants.defaultRotationSpeed;

    public float RotationSpeed
    {
        get => rotationSpeed;
    }
}