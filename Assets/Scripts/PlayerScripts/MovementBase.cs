using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    public virtual MovementBase AddCurrentComponentToObject(GameObject targetObject)
    {
        Type currentType = this.GetType();
        return gameObject.AddComponent(currentType) as MovementBase;
    }
}