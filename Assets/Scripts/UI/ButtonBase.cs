using UnityEngine;
using System;
using CustomUtility;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    [SerializeField] protected Button button;

    protected virtual void Awake()
    {
        CheckReferences();
    }

    protected void OnEnable()
    {
        SubscribeEvents();
    }

    protected void OnDisable()
    {
        UnsubscribeEvents();
    }

    protected virtual void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref button, gameObject, nameof(button));
    }

    protected abstract void SubscribeEvents();
    
    protected abstract void UnsubscribeEvents();

}