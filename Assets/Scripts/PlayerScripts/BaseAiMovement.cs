using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAiMovement<TMovementSO> : PlayerMovement<TMovementSO> where TMovementSO : PlayerMovementSO
{
    protected readonly Queue<Action> movementActions = new Queue<Action>(2);
    protected EventBinding<AiJumped> onAiJumped;

    protected override void Update()
    {
        if (movementActions.Count > 0)
        {
            Debug.Log($"invoke");
            movementActions.Dequeue().Invoke();
        }
        MoveForward();
    }
    
    protected override void SubscribeEvents()
    {
        onAiJumped = new EventBinding<AiJumped>(RequestJump);
        EventBus<AiJumped>.Register(onAiJumped);
    }

    protected override void UnSubscribeEvents()
    {
        EventBus<AiJumped>.DeRegister(onAiJumped);
    }
    
    /// <summary>
    /// Queue up a certain number of movement actions that will take place during the coming frames
    /// </summary>
    /// <param name="actions">How many actions shall be queued.
    /// Proportional to how many frames this action will be performed</param>
    protected virtual void RequestJump(AiJumped actions)
    {
        for (int i = 0; i < actions.NrJumpActions; i++)
        {
            movementActions.Enqueue(Jump);    
        }
    }
    
}