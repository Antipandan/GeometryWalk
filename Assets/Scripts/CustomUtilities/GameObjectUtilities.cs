using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CustomUtility
{
    public static class GameObjectUtilities
    {
        public static void RemoveCertainComponents(params Component[] components)
        {
            for (int i = 0; i < components.Length; i++)
            {
                Component component = components[i];
                Object.Destroy(component);
            }
        }

        public static void RemoveCertainGameObjects(params GameObject[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                GameObject gameObject = gameObjects[i];
                Object.Destroy(gameObject);
            }
        }

        public static void CopyComponentValue<TComponent>(ref GameObject source, ref GameObject target,
            TComponent component) where TComponent : Component
        {
            Debug.Log($"source: {source.name}, target: {target.name}");
            Type componentType = component.GetType();
            Component sourceComponent = source.GetComponent(componentType);
        }
    }
}