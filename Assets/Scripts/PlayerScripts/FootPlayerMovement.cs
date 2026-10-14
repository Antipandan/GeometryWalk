using System;
using UnityEngine;

public sealed class FootPlayerMovement : PlayerMovement
{
    [SerializeField] private LayerMask validGroundLayers;
    private bool isGrounded = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        SubscribeEvents();
    }

    protected override void Update()
    {
        MoveForward();
        if (jumpAction.WasPerformedThisFrame() && isGrounded) Jump();
    }

    private void UpdateIsGrounded(Collision2D collision2D)
    {
        if (!isGrounded && validGroundLayers.HasLayer(collision2D.gameObject.layer))
        {
            isGrounded = true;
        }
    }

    private void SubscribeEvents()
    {
        player.OnCollision2D += UpdateIsGrounded;
    }

    protected override void Jump()
    {
        if (!isGrounded) return;
        if (rb != null)
        {
            rb.linearVelocity += new Vector2(0, UpwardsForce);
        }
        isGrounded = false;
    }
}