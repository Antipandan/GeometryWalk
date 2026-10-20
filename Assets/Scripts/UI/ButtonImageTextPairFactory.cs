using NUnit.Framework.Interfaces;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageTextPairFactory : CustomUICreator
{
    public override GameObject CreateInstance()
    {
        GameObject buttonImageTextPair = new GameObject("ButtonImageTextPairFactory");
        AddNecessaryComponents(buttonImageTextPair);
        GameObject icon = new GameObject("Icon");
        AddNecessaryIconComponents(icon);
        GameObject text = new GameObject("Text");
        AddNecessaryTextComponents(text);
        icon.transform.SetParent(buttonImageTextPair.transform);
        text.transform.SetParent(buttonImageTextPair.transform);
        float heightOffset = text.GetComponent<RectTransform>().rect.height;
        text.transform.position += new Vector3(0, heightOffset * 3/2f, 0);
        return buttonImageTextPair;
    }

    private static void AddNecessaryComponents(GameObject button)
    {
        CustomUIBuilder.AddNecessaryUIButtonComponents(button);
        Image image = button.AddComponent<Image>();
        image.sprite = CustomUIBuilder.LoadDefaultUIBackgroundSprite();
    }

    private static void AddNecessaryIconComponents(GameObject icon)
    {
        CustomUIBuilder.AddNecessaryUIComponents(icon);
        Image image = icon.AddComponent<Image>();
        Sprite defaultImageSprite = CustomUIBuilder.LoadDefaultUIIconSprite();
        image.sprite = defaultImageSprite;
        image.color = Color.black;
    }

    private static void AddNecessaryTextComponents(GameObject text)
    {
        CustomUIBuilder.AddNecessaryUIComponents(text);
        TextMeshProUGUI meshPro = text.AddComponent<TextMeshProUGUI>();
        meshPro.text = "text";
        meshPro.alignment = TextAlignmentOptions.Center;
    }
    
}