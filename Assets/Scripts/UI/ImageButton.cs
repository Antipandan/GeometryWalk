using System;
using CustomUtility;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(CanvasRenderer), typeof(Button))]
public class ButtonImage : MonoBehaviour
{
    #region StaticMethods

    public static ButtonImage CreateInstance()
    {
        ButtonImage image = new GameObject("ButtonImage").AddComponent<ButtonImage>();
        AddNecessaryComponents(image);
        GameObject child = new GameObject("Icon");
        AddNecessaryChildComponents(child);
        child.transform.SetParent(image.transform);
        child.transform.position = image.transform.position;
        return image;
    }

    private static void AddNecessaryComponents(ButtonImage button)
    {
        Image image = button.gameObject.AddComponent<Image>();
        configure(image);
        return;

        static void configure(Image image)
        {
            Sprite loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>
                ("Assets/TWD/FreeButtonSet/Textures/other/universal_panel_40.png");
            if (loadedSprite == null) Logging.LogNullReferenceError("sprite", ErrorSeverity.Warning, image.gameObject, () => {
                Debug.Log("This asset could not be found");});
            image.sprite ??= loadedSprite;
            image.color = Color.white;
        }
    }

    private static void AddNecessaryChildComponents(GameObject go)
    {
        Image icon = go.AddComponent<Image>();
        Sprite loadedIcon = AssetDatabase.LoadAssetAtPath<Sprite>
            ("Assets/TWD/FreeButtonSet/Textures/icons/128x128/repeat.png");
        if (loadedIcon == null) Logging.LogNullReferenceError("sprite", ErrorSeverity.Warning, icon.gameObject, () => {
            Debug.Log("This asset could not be found");});
        icon.sprite ??= loadedIcon;
        icon.color = Color.black;
    }

    #endregion

}
