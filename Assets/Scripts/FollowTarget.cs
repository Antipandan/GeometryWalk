using UnityEngine;
using System;
using CustomUtility;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset = Vector2.zero;

    private void Awake()
    {
        CheckReferences();
    }

    private void Update()
    {
        Follow();
    }
    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref target, gameObject, nameof(target));
    }

    private void Follow()
    {
        if (target == null) return;
        gameObject.transform.position = new Vector3(target.position.x + offset.x, gameObject.transform.position.y + offset.y, -10);
    }

    public void CalculateOffset()
    {
        if (target == null) return;
        Vector2 deltaDistance = gameObject.transform.position - target.position;
        offset.x = deltaDistance.x;
    }

}