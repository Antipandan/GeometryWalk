using UnityEngine;
using System;
using System.Collections;
using CustomUtility;
using Random = System.Random;

public sealed class AiPlayer : PlayerMovementBase
{
    [SerializeField] [Range(0, 1<<31 - 1)] private int stateNumber = 11;
    [SerializeField] [Range(0f, 60000f)][Tooltip("Delay in milliseconds")] private float thinkingDelay = 50f;
    private Random rand;
    
    public float ThinkingDelay
    {
        get => thinkingDelay;
    }

    public float ThinkingDelaySeconds
    {
        get => thinkingDelay / 1000f;
    }

    protected override void Awake()
    {
        base.Awake();
        SetupValues();
    }

    protected override void OnEnable()
    {
        StartCoroutine(Thinking());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Thinking()
    {
        while (true)
        {
            yield return new WaitForSeconds(ThinkingDelaySeconds);
            ushort nrActions = (ushort)rand.Next(0, 3);
            AiJumped action = new AiJumped(nrActions);
            EventBus<AiJumped>.Raise(action);
        }

        yield break;
    }

    private void SetupValues()
    {
        rand = new Random(stateNumber);
    }
}