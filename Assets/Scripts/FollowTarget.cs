using UnityEngine;
using System;
using CustomUtility;
using DefaultNamespace.Events;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset = Vector2.zero;
    private EventBinding<FollowObjectChanged> changeTarget;
    private void Awake()
    {
        CheckReferences();
        changeTarget = new EventBinding<FollowObjectChanged>(ChangeFollowTarget);
    }

    private void OnEnable()
    {
        EventBus<FollowObjectChanged>.Register(changeTarget);
    }

    private void OnDisable()
    {
        EventBus<FollowObjectChanged>.DeRegister(changeTarget);
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

    private void ChangeFollowTarget(FollowObjectChanged newFollowObject)
    {
        target = newFollowObject.NewFollowObject.transform;
    }

    public void CalculateOffset()
    {
        if (target == null) return;
        Vector2 deltaDistance = gameObject.transform.position - target.position;
        offset.x = deltaDistance.x;
    }

}