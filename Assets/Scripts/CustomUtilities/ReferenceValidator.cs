using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CustomUtility
{
    public static class ReferenceValidator
    {
        /// <summary>
        /// Checks the given component if null and logs a standardized message in the Unity terminal
        /// </summary>
        /// <param name="component">The component to check for null</param>
        /// <param name="gameObject">The gameObject that this component is attached to</param>
        /// <param name="componentName">The name of the component</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TComponent">The type of the component being checked for null</typeparam>
        public static void CheckComponentForNull<TComponent>(ref TComponent component, GameObject gameObject,
            string componentName, ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true, bool preventPlay = false) where TComponent : Component
        {
            component ??= gameObject.GetComponent<TComponent>();
            if (component == null && gameObject != null && logResult) Logging.LogNullReferenceError(componentName, severity, gameObject);
            if (preventPlay) ErrorHandling.ExitPlayModeProperly();
        }
        
        /// <summary>
        /// Checks the given component if null and logs a standardized message in the Unity terminal
        /// </summary>
        /// <param name="component">The component to check for null</param>
        /// <param name="gameObject">The gameObject that this component is attached to</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TComponent">The type of the component being checked for null</typeparam>
        public static void CheckComponentForNull<TComponent>(ref TComponent component, GameObject gameObject,
            ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true, bool preventPlay = false) where TComponent : Component
        {
            CheckComponentForNull(ref component, gameObject, nameof(component), severity, logResult, preventPlay);
        }
        
        /// <summary>
        /// Checks the given component if null and logs a standardized message in the Unity terminal
        /// </summary>
        /// <param name="component">The component to check for null</param>
        /// <param name="gameObject">The gameObject that this component is attached to</param>
        /// <param name="wasNull">Was the component null</param>
        /// <param name="componentName">The name of the component</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TComponent">The type of the component being checked for null</typeparam>
        public static void CheckComponentForNull<TComponent>(ref TComponent component, GameObject gameObject, out bool wasNull,
            string componentName = nameof(Component), ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true, bool preventPlay = false) where TComponent : Component
        {
            component ??= gameObject.GetComponent<TComponent>();
            if (component == null && gameObject != null)
            {
                if (logResult) Logging.LogNullReferenceError(componentName, severity, gameObject);
                wasNull = true;
            }
            else wasNull = false;
            if (preventPlay) ErrorHandling.ExitPlayModeProperly();
        }
        
        /// <summary>
        /// Checks the given component if null and logs a standardized message in the Unity terminal.
        /// It is assumed that said component is in a child gameObject
        /// </summary>
        /// <param name="component">The component to check for null</param>
        /// <param name="gameObject">The gameObject that this component is attached to</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TComponent">The type of the component being checked for null</typeparam>
        public static void CheckComponentForNullChild<TComponent>(ref TComponent component, GameObject gameObject,
            ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true, bool preventPlay = false) where TComponent : Component
        {
            component ??= gameObject.GetComponentInChildren<TComponent>();
            if (component == null && gameObject != null && logResult) Logging.LogNullReferenceError(nameof(component), severity, gameObject);
            if (preventPlay) ErrorHandling.ExitPlayModeProperly();
        }

        /// <summary>
        /// Checks the given component if null and logs a standardized message in the Unity terminal.
        /// It is assumed that said component is in a child gameObject
        /// </summary>
        /// <param name="component">The component to check for null</param>
        /// <param name="gameObject">The gameObject that this component is attached to</param>
        /// <param name="componentName">The name of the component</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TComponent">The type of the component being checked for null</typeparam>
        public static void CheckComponentForNullChild<TComponent>(ref TComponent component, GameObject gameObject,
            string componentName, ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true,
            bool preventPlay = false) where TComponent : Component
        {
            component ??= gameObject.GetComponentInChildren<TComponent>();
            if (component == null && gameObject != null && logResult) Logging.LogNullReferenceError(componentName, severity, gameObject);
            if (preventPlay) ErrorHandling.ExitPlayModeProperly();
        }

        /// <summary>
        /// Checks if a UnityEngine.Object is null. If so logs a standardized message in the Unity terminal
        /// </summary>
        /// <param name="object">UnityEngine.Object to check for null</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TObject">The type of the UnityEngine.Object being checked for null</typeparam>
        public static void CheckUnityObjectForNull<TObject>(ref TObject @object,
            ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true, bool preventPlay = false) where TObject : Object
        {
            if (@object == null && logResult) Logging.LogNullReferenceError(nameof(@object), severity);
            if (preventPlay) ErrorHandling.ExitPlayModeProperly();
        }

        /// <summary>
        /// Checks if a UnityEngine.Object is null. If so logs a standardized message in the Unity terminal
        /// </summary>
        /// <param name="object">UnityEngine.Object to check for null</param>
        /// <param name="objectName">The name of the UnityEngine.Object</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TObject">The type of the UnityEngine.Object being checked for null</typeparam>
        public static void CheckUnityObjectForNull<TObject>(ref TObject @object, string objectName,
            ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true, bool preventPlay = false) where TObject : Object
        {
            if (@object == null && logResult) Logging.LogNullReferenceError(objectName, severity);
            if (preventPlay) ErrorHandling.ExitPlayModeProperly();
        }
        
        /// <summary>
        /// Checks the given component if null and logs a standardized message in the Unity terminal.
        /// Returns the component that was found
        /// </summary>
        /// <param name="gameObject">The gameObject that this component is attached to</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TComponent">The type of the component being checked for null</typeparam>
        [CanBeNull]
        public static TComponent FindComponentInGameObject<TComponent>(GameObject gameObject, 
            ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true, bool preventPlay = false)
            where TComponent : Component
        {
            TComponent searchComponent = gameObject.GetComponent<TComponent>();
            CheckComponentForNull(ref searchComponent, gameObject, nameof(searchComponent),
                severity, logResult, preventPlay);
            return searchComponent;
        }

        /// <summary>
        /// Checks the given component if null and logs a standardized message in the Unity terminal.
        /// Returns the component that was found
        /// </summary>
        /// <param name="gameObject">The gameObject that this component is attached to</param>
        /// <param name="componentName">The name of the component</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TComponent">The type of the component being checked for null</typeparam>
        public static TComponent FindComponentInGameObject<TComponent>(GameObject gameObject, string componentName,
            ErrorSeverity severity = ErrorSeverity.Error, bool logResult = true, bool preventPlay = false) where TComponent : Component
        {
            TComponent searchComponent = gameObject.GetComponent<TComponent>();
            CheckComponentForNull(ref searchComponent, gameObject, componentName, 
                severity,logResult, preventPlay);
            return searchComponent;
        }

        /// <summary>
        /// Checks the given component if null and logs a standardized message in the Unity terminal.
        /// Returns the component that was found
        /// </summary>
        /// <param name="gameObject">The gameObject that this component is attached to</param>
        /// <param name="wasNull">Was the component null</param>>
        /// <param name="componentName">The name of the component</param>
        /// <param name="severity">How severe should the null reference be</param>
        /// <param name="logResult">Should the function log the message in the terminal</param>
        /// <param name="preventPlay">Should the null reference prevent users from entering playmode</param>
        /// <typeparam name="TComponent">The type of the component being checked for null</typeparam>
        [CanBeNull]
        public static TComponent FindComponentInGameObject<TComponent>(GameObject gameObject, out bool wasNull,
            string componentName, ErrorSeverity severity = ErrorSeverity.Error,
            bool logResult = true, bool preventPlay = false) where TComponent : Component
        {
            TComponent searchComponent = gameObject.GetComponent<TComponent>();
            CheckComponentForNull(ref searchComponent, gameObject, out bool @null,
                componentName, severity, logResult, preventPlay);
            wasNull = @null;
            return searchComponent;
        }
    }   

}