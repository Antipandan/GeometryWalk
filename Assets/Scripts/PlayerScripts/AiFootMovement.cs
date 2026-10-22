using System;
using System.Collections;
using CustomUtility;
using UnityEngine;

public sealed class AiFootMovement : BaseAiMovement<PlayerMovementFootSO>
{
    [SerializeField] private LayerMask validGroundLayers;
    [SerializeField] private AngleLerper angleLerper;
    private bool isGrounded = false;
    
    protected override void CheckReferences()
    {
        base.CheckReferences();
        ReferenceValidator.CheckComponentForNull(ref angleLerper, gameObject, nameof(angleLerper));
    }

    protected override void RequestJump(AiJumped action)
    {
        Debug.Log($"request jump");
        if (!isGrounded) return;
        Debug.Log($"can jump");
        for (int i = 0; i < action.NrJumpActions; i++)
        { 
            movementActions.Enqueue(Jump);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DecideIfGrounded(other);
    }

    private void DecideIfGrounded(Collision2D collision2D)
    {
        if (isGrounded || !validGroundLayers.HasLayer(collision2D.gameObject.layer)) return;
        if (this == null) return;
        isGrounded = true;
        StartCoroutine(angleLerper.LerpClampRotation(gameObject));
    }
}