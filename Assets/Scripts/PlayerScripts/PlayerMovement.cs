using UnityEngine;
using System;
using System.Collections.Generic;
using System.Numerics;
using CustomUtility;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public abstract class PlayerMovement<TPlayerMovementSo> : MonoBehaviour where TPlayerMovementSo : PlayerMovementSO
{
    [SerializeField] protected TPlayerMovementSo playerMovementData;
    [SerializeField] protected Rigidbody2D rb;
    protected float movementSpeed = 0f;
    protected float UpwardsForce = 0f;
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
        SetupValues();
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

    protected virtual void SetupValues()
    {
        if (playerMovementData == null) return;
        movementSpeed = playerMovementData.MovementSpeed;
        UpwardsForce = playerMovementData.UpwardsForce;
    }

    public virtual Type GetMovementDataType()
    {
        return playerMovementData.GetType();
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
        ReferenceValidator.CheckObjectForNull(ref playerMovementData, gameObject, nameof(playerMovementData));
        ReferenceValidator.CheckComponentForNull(ref rb, gameObject, nameof(rb));
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

    public virtual void SetDefaultValues()
    {
        this.movementSpeed = GameConstants.defaultMovementSpeed;
        this.UpwardsForce = GameConstants.defaultUpwardsForce;
        this.jumpAction = InputSystem.actions.FindAction("Jump");
        this.rb = gameObject.GetComponent<Rigidbody2D>();
        if (this.rb == null) Logging.LogRegularStringMessage("rb not found", ErrorSeverity.Warning, gameObject);
    }
    
}