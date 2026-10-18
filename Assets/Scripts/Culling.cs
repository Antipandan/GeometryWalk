using System;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class Culling : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }

        private void OnBecameVisible()
        {
            gameObject.SetActive(true);
        }
    }
}