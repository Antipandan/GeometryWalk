using System;
using CustomUtility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImage : CustomUICreator
{
    
    #region PublicMethods

    public override GameObject CreateInstance()
    {
        GameObject image = new GameObject("ButtonImage");
        AddNecessaryComponents(image);
        GameObject child = new GameObject("Icon");
        AddNecessaryChildComponents(child);
        child.transform.SetParent(image.transform);
        child.transform.position = image.transform.position;
        return image;
    }

    #endregion
    #region StaticMethods

    private static void AddNecessaryComponents(GameObject button)
    {
        Image image = CustomUIBuilder.AddNecessaryUIButtonComponents(button).AddComponent<Image>();
        configure(image);
        return;

        static void configure(Image image)
        {
            Sprite loadedSprite = CustomUIBuilder.LoadDefaultUIBackgroundSprite();
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
