using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public sealed class ImageSliderTextFactory : CustomUICreator
{
    public override GameObject CreateInstance()
    {
        GameObject slider = new GameObject("Slider");
        RectTransform rt = MakeSlider(ref slider);
        rt.offsetMin = new Vector2(0f, 0f);
        rt.offsetMax = new Vector2(160f, 20f);
        GameObject text = new GameObject("Text");
        TextMeshProUGUI textMesh = SetupText(text);
        PositionText(textMesh);
        textMesh.transform.SetParent(slider.transform);
        
        return slider;
    }

    private static RectTransform MakeSlider(ref GameObject slider)
    {
        // Transform parentTransform = slider.transform; // this doesn't work. Inlining does
        Slider newSlider = CustomUIBuilder.AddNecessaryUIComponents(slider).AddComponent<Slider>();
        GameObject background = new GameObject("Background");
        Image image = CustomUIBuilder.AddNecessaryUIComponents(background).AddComponent<Image>();
        image.transform.SetParent(slider.transform);
        SetupBackground(image);
        RectTransform fillArea = new GameObject("Fill Area").AddComponent<RectTransform>();
        fillArea.transform.SetParent(slider.transform);
        SetupFillArea(fillArea);
        GameObject fill = new GameObject("Fill");
        fill.transform.SetParent(fillArea.transform);
        // Image imageFill = SetupFill(fill);
        GameObject handleSlideArea = new GameObject("Handle Slide Area");
        handleSlideArea.transform.SetParent(slider.transform);
        SetupHandleSlideArea(handleSlideArea);
        GameObject handle = new GameObject("Handle");
        handle.transform.SetParent(handleSlideArea.transform);
        Image imageHandle = SetupHandle(handle);
        CustomUIBuilder.AddNecessaryUIComponents(fill);
        RectTransform trans = fill.GetComponent<RectTransform>();
        newSlider.fillRect = trans;
        newSlider.handleRect = imageHandle.rectTransform;
        newSlider.minValue = 0f;
        newSlider.maxValue = 1f;
        newSlider.value = 0f;
        return slider.GetComponent<RectTransform>();
    }

    private static Image SetupHandle(GameObject handle)
    {
        Sprite handleSprite = CustomUIBuilder.LoadDefaultSliderBackground();
        Image image = CustomUIBuilder.AddNecessaryUIComponents(handle).AddComponent<Image>();
        image.rectTransform.anchorMin = new Vector2(0f, 0f);
        image.rectTransform.anchorMax = new Vector2(0f, 1f);
        image.rectTransform.offsetMin = new Vector2(0f, 0f);
        image.rectTransform.offsetMax = new Vector2(20f, 0f);
        image.sprite = handleSprite;
        return image;
    }

    private static RectTransform SetupHandleSlideArea(GameObject handleSlideArea)
    {
        RectTransform transform = handleSlideArea.AddComponent<RectTransform>();
        transform.anchorMin = new Vector2(0f, 0f);
        transform.anchorMax = new Vector2(1f, 1f);
        transform.offsetMin = new Vector2(0f, 0f);
        transform.offsetMax = new Vector2(-10f, 0f);
        return transform;
    }

    private static Image SetupFill(GameObject fill)
    {
        Sprite FillSprite = CustomUIBuilder.LoadDefaultUIBackgroundSprite();
        Image image = CustomUIBuilder.AddNecessaryUIComponents(fill).AddComponent<Image>();
        image.rectTransform.anchorMin = new Vector2(0f, 0f);
        image.rectTransform.anchorMax = new Vector2(0f, 1f);
        image.rectTransform.offsetMin = new Vector2(0f, 0f);
        image.rectTransform.offsetMax = new Vector2(10f, 0f);
        image.color = Color.black;
        image.sprite = FillSprite;
        return image;
    }

    private static RectTransform SetupFillArea(RectTransform fillArea)
    {
        fillArea.anchorMin = new Vector2(0f, 0.25f);
        fillArea.anchorMax = new Vector2(1f, 0.75f);
        fillArea.offsetMin = new Vector2(0f, 0f);
        fillArea.offsetMax = new Vector2(-15f, 0f);
        return fillArea;
    }

    private static Image SetupBackground(Image background)
    {
        Sprite backgroundSprite = CustomUIBuilder.LoadDefaultSliderBackground();
        background.color = Color.white;
        background.rectTransform.anchorMin = new Vector2(0f, 0.25f);
        background.rectTransform.anchorMax = new Vector2(1f, 0.75f);
        background.rectTransform.offsetMin = new Vector2(0f, 0f);
        background.rectTransform.offsetMax = new Vector2(0f, 0f);
        background.raycastTarget = true;
        background.maskable = true;
        background.sprite = backgroundSprite;
        return background;
    }

    private static TextMeshProUGUI SetupText(GameObject text)
    {
        TextMeshProUGUI mesh = CustomUIBuilder.AddNecessaryUIComponents(text).AddComponent<TextMeshProUGUI>();
        mesh.text = "Text";
        mesh.rectTransform.offsetMin = new Vector2(0f, 0f);
        mesh.rectTransform.offsetMax = new Vector2(160f, 40f);
        mesh.alignment = TextAlignmentOptions.Center;
        return mesh;
    }

    private static void PositionText(TextMeshProUGUI text)
    {
        float height = text.rectTransform.rect.height;
        Vector3 newPosition = text.rectTransform.position;
        newPosition = new Vector3(newPosition.x, height, newPosition.z);
        text.rectTransform.position = newPosition;
    }
}