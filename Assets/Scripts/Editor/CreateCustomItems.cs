using System;
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
            CreateCustomUIElements<ButtonImage>(command);
        }

        [MenuItem("GameObject/UI (Canvas)/Button - ImageTextPair", false, 10)]

        public static void CreateImageButtonTextPair(MenuCommand command)
        {
            CreateCustomUIElements<ButtonImageTextPairFactory>(command);
        }

        [MenuItem("GameObject/UI (Canvas)/Slider - Text", false, 10)]

        public static void CreateSliderText(MenuCommand command)
        {
            CreateCustomUIElements<ImageSliderTextFactory>(command);
        }

        private static void CreateCustomUIElements<TCustomUiElement>(MenuCommand command)
            where TCustomUiElement : CustomUICreator, new()
        {
            TCustomUiElement builder = new TCustomUiElement();
            GameObject customGameObject = builder.CreateInstance();
            // dont know
            GameObject parent = (GameObject)command.context;
            // parent if needed
            GameObjectUtility.SetParentAndAlign(customGameObject.gameObject, parent);
            // undo work
            Undo.RegisterCreatedObjectUndo(customGameObject.gameObject, "Create " + customGameObject.name);
            // selected object
            Selection.activeObject = customGameObject;
        }
    }
}