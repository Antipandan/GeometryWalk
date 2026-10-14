using UnityEngine;
using System;
using CustomUtility;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;


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
        gameObject.transform.position = new Vector3(target.position.x, gameObject.transform.position.y, -10);
    }

}