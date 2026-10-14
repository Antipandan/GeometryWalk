using System.Collections;
using UnityEngine;

public interface IDelayedDestruction
{
    public float DestructionDelay { get; set; }
    
    public GameObject @GameObject { get; }

    public IEnumerator DestroyObjectDelayed()
    {
        yield return new WaitForSeconds(DestructionDelay);
        Object.Destroy(@GameObject);
    }

    #region Builders

    public IDelayedDestruction ChangeTransformValues(Transform transform)
    {
        GameObject.transform.position = transform.position;
        GameObject.transform.rotation = transform.rotation;
        GameObject.transform.localScale = transform.localScale;
        return this;
    }
    
    public IDelayedDestruction ChangePosition(Vector3 position)
    {
        GameObject.transform.position = position;
        return this;
    }

    public static IDelayedDestruction CreateInstance(IDelayedDestruction impl)
    {
        Object.Instantiate(impl.GameObject);
        return impl;
    }

    #endregion

}