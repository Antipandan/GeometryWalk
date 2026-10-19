using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] [Tooltip("What GameObject shall this GameObject follow?")] private GameObject followTarget;

    private void Update()
    {
        FollowTarget();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void FollowTarget()
    {
        if (followTarget != null) gameObject.transform.position = followTarget.transform.position;
    }
}