using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask damageMask;
    private void OnCollisionEnter2D(Collision2D other)
    { 
        if (damageMask.HasLayer(other.gameObject.layer))
        {
            KillPlayer();    
        }
    }
    
    private void KillPlayer()
    {
        Debug.Log($"kill!");
    }
}