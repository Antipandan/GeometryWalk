using Unity.VisualScripting;
using UnityEditor;

namespace CustomUtility
{
    public static class ErrorHandling
    {

        /// <summary>
        /// Custom method that calls several methods that should exit playmode
        /// </summary>
        public static void ExitPlayModeProperly()
        {
#if UNITY_EDITOR
            EditorApplication.isPaused = true;
            EditorApplication.delayCall += EditorApplication.ExitPlaymode;
#endif
        }
        
        /// <summary>
        /// Displays a pop out window when something has presumably gone wrong
        /// </summary>
        /// <param name="warningMessage">Message to be displayed on the window.
        /// Message is part of a standardized message</param>
        /// <returns>True if user clicked button. False if not. Users don't have to use the return value</returns>
        public static bool DisplayWarning(string warningMessage)
        {
            #if UNITY_EDITOR
            return EditorUtility.DisplayDialog("Warning", warningMessage, "OK");
            #endif
            return true;
        }

        /// <summary>
        /// Prevents entering playmode and displays a message as to why that was the case
        /// </summary>
        /// <param name="warningMessage">Message to be displayed on the window.
        /// Message is part of a standardized message</param>
        public static void PreventEnteringPlayMode(string warningMessage = "Unable to enter play mode")
        {
#if UNITY_EDITOR
            DisplayWarning(warningMessage);
            // we are now doing two checks of the same thing but this is in editor
            ExitPlayModeProperly();
#endif
        }
    }
}