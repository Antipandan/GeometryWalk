using System.Runtime.CompilerServices;
using CustomUtility;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CustomUIBuilder
{
    public static GameObject AddNecessaryUIComponents(GameObject UIObject)
    {
        InternalAddNecessaryUIComponents(UIObject);
        return UIObject;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void InternalAddNecessaryUIComponents(GameObject UIObject)
    {
        UIObject.AddComponent<RectTransform>();
        UIObject.AddComponent<CanvasRenderer>();
    }

    public static GameObject AddNecessaryUIButtonComponents(GameObject UIObject)
    {
        InternalAddNecessaryUIComponents(UIObject);
        UIObject.AddComponent<Button>();
        return UIObject;
    }

    [CanBeNull]
    public static Sprite LoadDefaultUIBackgroundSprite(bool LogResults = true)
    {
        Sprite loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>
            (GameConstants.defaultBackGroundPath);
        if (loadedSprite == null && LogResults) Logging.LogNullReferenceError("Default UI Sprite", ErrorSeverity.Error);
        return loadedSprite;
    }

    public static Sprite LoadDefaultUIIconSprite(bool LogResults = true)
    {
        Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>
            (GameConstants.defaultIconPath);
        if (LogResults && icon == null)  Logging.LogNullReferenceError("Default UI Icon Sprite", ErrorSeverity.Error);
        return icon;
    }

    public static Sprite LoadDefaultSliderBackground(bool LogResults = true)
    {
        Sprite background = AssetDatabase.LoadAssetAtPath<Sprite>(
            GameConstants.defaultSliderBackground);
        if (LogResults && background == null) Logging.LogNullReferenceError("Default UI Icon Sprite", ErrorSeverity.Error);
        return background;
    }
    
}