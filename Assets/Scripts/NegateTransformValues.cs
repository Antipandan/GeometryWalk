using System;
using CustomUtility;
using JetBrains.Annotations;
using UnityEngine;

public class NegateTransformValues : MonoBehaviour
{
    [SerializeField] [CanBeNull] private GameObject parentGameObject;
    [SerializeField] private bool negateTransformPosition = false;
    [SerializeField] private bool negateRotation = true;
    [SerializeField] private bool negateScale = false;

    private void Awake()
    {
        CheckReferences();
    }

    private void LateUpdate()
    {
        NegateComponents();
    }

    private void NegateComponents()
    {
        if (negateTransformPosition) NegateTransformPosition(parentGameObject, gameObject);
        else 
        if (negateRotation) NegateTransformRotation(parentGameObject, gameObject);
        if (negateScale) NegateTransformScale(parentGameObject, gameObject);
    }

    private void CheckReferences()
    {
        if (parentGameObject == null) parentGameObject = gameObject.GetComponentInParent<Transform>().gameObject;
    }

    public static void NegateTransformPosition(GameObject parent, GameObject child)
    {
        Transform parentTransform = parent.transform;
        Transform childTransform = child.transform;
        
        childTransform.position = Vector3.zero;
    }

    public static void NegateTransformRotation(GameObject parent, GameObject child)
    {
        Transform parentTransform = parent.transform;
        Transform childTransform = child.transform;

        Vector3 eulerAngles = Vector3.zero;
        childTransform.eulerAngles = eulerAngles;
    }

    public static void NegateTransformScale(GameObject parent, GameObject child)
    {
        Transform parentTransform = parent.transform;
        Transform childTransform = child.transform;
        
        childTransform.localScale = parentTransform.localScale.Divide(1f);
    }
    
}