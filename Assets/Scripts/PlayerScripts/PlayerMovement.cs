using UnityEngine;
using System;
using System.Collections.Generic;
using System.Numerics;
using CustomUtility;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public abstract class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Player player;
    [SerializeField] protected float movementSpeed = GameConstants.defaultMovementSpeed;
    [SerializeField] protected float UpwardsForce = GameConstants.defaultUpwardsForce;
    protected InputAction jumpAction;
    protected virtual float MovementSpeed
    {
        get => movementSpeed;
    }

    protected virtual void Awake()
    {
        CheckReferences();
    }

    protected virtual void OnEnable()
    {
        GetInputs();
        SubscribeEvents();
    }

    protected virtual void Update()
    {
        MoveForward();
        if (jumpAction.IsPressed()) Jump();
        ClampLinearVelocity();
    }

    protected virtual void OnDisable()
    {
        UnSubscribeEvents();
    }

    protected virtual void SubscribeEvents()
    {
        
    }

    protected virtual void UnSubscribeEvents()
    {
        
    }
    
    protected virtual void GetInputs()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        ReferenceValidator.CheckObjectForNull(ref jumpAction, gameObject, nameof(jumpAction), ErrorSeverity.Error, false, logMessage);
        return;

        void logMessage()
        {
            Logging.LogRegularStringMessage("jump action could not be found. This is probably because the" +
                                            " name of the JumpAction was changed. Original name was 'Jump'",
                ErrorSeverity.Warning, gameObject);
        }
    }
    protected virtual void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref rb, gameObject, nameof(rb));
        ReferenceValidator.CheckComponentForNullChild(ref player, gameObject, nameof(player));
    }

    protected virtual void MoveForward()
    {
        float movementSpeedX = movementSpeed * Time.deltaTime;
        gameObject.transform.position += Vector3.right * movementSpeedX;
    }

    protected virtual void ClampLinearVelocity()
    {
        rb.linearVelocity = rb.linearVelocity.Clamp(GameConstants.minVelocity, GameConstants.maxVelocity);
    }

    protected virtual void Jump()
    {
        if (rb != null)
        {
            rb.linearVelocity += new Vector2(0, UpwardsForce) * Time.deltaTime;
        }
    }
}