using UnityEngine;
using System;
using System.Collections.Generic;
public sealed class ShipPlayerMovement : PlayerMovement<PlayerMovementShipSO>
{
    [SerializeField] [Range(-100f, 100f)] private float rotationVelocityMultiplier = 3f;
    protected override void ConstructNewPlayer()
    {
        //main body
        GameObject player = new GameObject("Player");
        player.AddComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
        float xValue = (rb.linearVelocityY / 100f) * rotationVelocityMultiplier;
        float rotationZ = CalculateRotation(xValue);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationZ));
    }

    private float CalculateRotation(float xValue)
    {
        float derivativeTan = (Mathf.Atan(xValue) * Mathf.Atan(xValue)) + 1;
        float angle = Mathf.Asin(xValue / derivativeTan) * Mathf.Rad2Deg;
        return angle;
    }
}