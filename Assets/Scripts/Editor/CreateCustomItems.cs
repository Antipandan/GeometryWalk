using UnityEditor;
using UnityEngine;

namespace Editor
{
    /// <summary>
    /// Class to be used if you want to add a tab in create menu to create a custom thing.
    /// See code below for how to implement something similar
    /// </summary>
    public static class CreateCustomItems
    {
        [MenuItem("GameObject/UI (Canvas)/Button - Image", false, 10)]
        public static void CreateImageButton(MenuCommand command)
        {
            GameObject buttonImage = ButtonImage.CreateInstance();
            // dont know
            GameObject parent = (GameObject)command.context;
            // parent if needed
            GameObjectUtility.SetParentAndAlign(buttonImage.gameObject, parent);
            // undo work
            Undo.RegisterCreatedObjectUndo(buttonImage.gameObject, "Create " + buttonImage.name);
            // selected object
            Selection.activeObject = buttonImage;
        }
    }
}