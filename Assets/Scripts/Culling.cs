using System;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class Culling : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private void OnBecameInvisible()
        {
            Debug.Log($"invis");
            gameObject.SetActive(false);
        }

        private void OnBecameVisible()
        {
            gameObject.SetActive(true);
        }
    }
}