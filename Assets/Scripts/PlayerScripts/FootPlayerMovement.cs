using System;
using UnityEngine;

public sealed class FootPlayerMovement : PlayerMovement
{
    [SerializeField] private LayerMask validGroundLayers;
    [SerializeField] private float rotationSpeed = GameConstants.defaultRotationSpeed;
    private bool isGrounded = false;

    protected override void Update()
    {
        MoveForward();
        if (jumpAction.IsPressed() && isGrounded) Jump();
        if (!isGrounded) RotatePlayerMidAir();
    }

    private void UpdateIsGrounded(Collision2D collision2D)
    {
        if (!isGrounded && validGroundLayers.HasLayer(collision2D.gameObject.layer))
        {
            isGrounded = true;
            ClampRotation();
        }
    }

    private void ClampRotation()
    {
        // ReSharper disable once PossibleLossOfFraction
        float newRotation = ((int)gameObject.transform.rotation.eulerAngles.z / 90) * 90f;
        Quaternion rotation = gameObject.transform.rotation;
        rotation.eulerAngles = new Vector3(0, 0, newRotation);
        gameObject.transform.rotation = rotation;
    }

    protected override void SubscribeEvents()
    {
        EventBus<Collision2D>.Register(new EventBinding<Collision2D>(UpdateIsGrounded));
    }

    private void RotatePlayerMidAir()
    {
        gameObject.transform.Rotate(0,0, -rotationSpeed * Time.deltaTime);
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