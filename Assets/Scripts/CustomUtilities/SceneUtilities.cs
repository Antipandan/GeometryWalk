using System;
using UnityEngine;

namespace CustomUtility
{
    [CreateAssetMenu(fileName = "SceneUtilities", menuName = "Scriptable Objects/SceneUtilities")]
    public class SceneUtilities : ScriptableObject
    {
        private static SceneUtilities instance;

        public static SceneUtilities Instance
        {
            get => instance;
        }
        
        private void Awake()
        {
            CheckReferences();
        }

        public void ResetRotation(GameObject @object)
        {
            @object.transform.rotation = Quaternion.identity;
        }

        private void CheckSingleTon()
        {
            if (instance == null) instance = this;
            else
            {
                Logging.LogSingletonError(nameof(SceneUtilities), ErrorSeverity.Warning);
                Destroy(this);
            }
        }

        private void CheckReferences()
        {
            CheckSingleTon();
        }
    }
}