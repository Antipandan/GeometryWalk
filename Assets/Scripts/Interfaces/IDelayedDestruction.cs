using System.Collections;
using UnityEngine;

public interface IDelayedDestruction
{
    public float DestructionDelay { get; set; }
    
    public GameObject @GameObject { get; }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(DestructionDelay);
        Object.Destroy(@GameObject);
    }
}