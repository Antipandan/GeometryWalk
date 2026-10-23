using System;
using System.Collections;
using CustomUtility;
using UnityEngine;

public sealed class AiFootMovement : BaseAiMovement<PlayerMovementFootSO>
{
    [SerializeField] private LayerMask validGroundLayers;
    [SerializeField] private AngleLerper angleLerper;
    private bool isGrounded = false;
    private float rotationSpeed = 0f;
    private EventBinding<Collision2D> onCollision2DEvent;

    protected override void Awake()
    {
        base.Awake();
        onCollision2DEvent = new EventBinding<Collision2D>(DecideIfGrounded);
    }

    protected override void CheckReferences()
    {
        base.CheckReferences();
        ReferenceValidator.CheckComponentForNull(ref angleLerper, gameObject, nameof(angleLerper));
    }

    protected override void RequestJump(AiJumped action)
    {
        for (int i = 0; i < action.NrJumpActions; i++)
        { 
            movementActions.Enqueue(Jump);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!isGrounded) RotatePlayermidAir();
    }

    private void RotatePlayermidAir()
    {
        gameObject.transform.Rotate(0,0, -rotationSpeed * Time.deltaTime);
    }

    protected override void Jump()
    {
        if (rb == null || !isGrounded) return;
        rb.linearVelocity += new Vector2(0, UpwardsForce);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DecideIfGrounded(other);
    }

    protected override void SetupValues()
    {
        base.SetupValues();
        if (playerMovementData != null)
        {
            rotationSpeed = playerMovementData.RotationSpeed;
        }
    }

    private void DecideIfGrounded(Collision2D collision2D)
    {
        if (isGrounded || !validGroundLayers.HasLayer(collision2D.gameObject.layer)) return;
        if (this == null) return;
        isGrounded = true;
        StartCoroutine(angleLerper.LerpClampRotation(gameObject));
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventBus<Collision2D>.Register(onCollision2DEvent);
    }

    protected override void UnSubscribeEvents()
    {
        base.UnSubscribeEvents();
        EventBus<Collision2D>.DeRegister(onCollision2DEvent);
    }
    
    protected override void ConstructNewPlayer()
    {
        return;
    }
}