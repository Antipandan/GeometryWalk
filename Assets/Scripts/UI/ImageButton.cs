using System;
using CustomUtility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonImage
{
    #region StaticMethods
    
    public static GameObject CreateInstance()
    {
        GameObject image = new GameObject("ButtonImage");
        AddNecessaryComponents(image);
        GameObject child = new GameObject("Icon");
        AddNecessaryChildComponents(child);
        child.transform.SetParent(image.transform);
        child.transform.position = image.transform.position;
        return image;
    }

    private static void AddNecessaryComponents(GameObject button)
    {
        button.AddComponent<RectTransform>();
        button.AddComponent<CanvasRenderer>();
        button.AddComponent<Button>();
        Image image = button.gameObject.AddComponent<Image>();
        configure(image);
        return;

        static void configure(Image image)
        {
            Sprite loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>
                (GameConstants.defaultBackGroundPath);
            if (loadedSprite == null) Logging.LogNullReferenceError("sprite", ErrorSeverity.Warning, image.gameObject, () => {
                Debug.Log("This asset could not be found", image.gameObject);});
            image.sprite ??= loadedSprite;
            image.color = Color.white;
        }
    }

    private static void AddNecessaryChildComponents(GameObject go)
    {
        Image icon = go.AddComponent<Image>();
        Sprite loadedIcon = AssetDatabase.LoadAssetAtPath<Sprite>
            (GameConstants.defaultIconPath);
        if (loadedIcon == null) Logging.LogNullReferenceError("sprite", ErrorSeverity.Warning, icon.gameObject, () => {
            Debug.Log("This asset could not be found", icon.gameObject);});
        icon.sprite ??= loadedIcon;
        icon.color = Color.black;
    }

    #endregion

}
