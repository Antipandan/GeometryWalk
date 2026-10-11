using UnityEngine;
using System;
using System.Collections.Generic;
using CustomUtility;
using UnityEngine.InputSystem;

public abstract class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;
    [SerializeField] private float movementSpeed = GameConstants.defaultMovementSpeed;

    protected virtual float MovementSpeed
    {
        get => movementSpeed;
    }

    protected virtual void Awake()
    {
        CheckReferences();
    }

    protected virtual void CheckReferences()
    {
        rb = GetComponent<Rigidbody2D>();
        ReferenceValidator.CheckComponentForNull(ref rb, gameObject, "Rigidbody");
        player = GetComponent<Player>();
        ReferenceValidator.CheckComponentForNull(ref player, gameObject, nameof(player));
    }

    protected virtual void MoveForward()
    {
        float movementSpeedX = movementSpeed * Time.deltaTime;
        gameObject.transform.Translate(movementSpeedX, 0, 0);
    }

    protected virtual void Update()
    {
        MoveForward();
    }

    public virtual void Jump(InputAction.CallbackContext context)
    {
        
    }
}