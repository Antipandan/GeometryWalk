using System;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"trigger enter by: {other.gameObject.name}");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log($"stay");
    }
}