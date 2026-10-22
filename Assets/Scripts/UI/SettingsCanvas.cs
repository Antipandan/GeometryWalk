using System;
using CustomUtility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class SettingsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI settingsText;
    [SerializeField] private Button goBackButton;
    [SerializeField] private MusicSlider mainMusicSlider;

    private void Awake()
    {
        CheckReferences();
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckUnityObjectForNull(ref holder, nameof(holder));
        ReferenceValidator.CheckComponentForNullChild(ref background, gameObject, nameof(background));
        ReferenceValidator.CheckComponentForNull(ref settingsText, gameObject, nameof(settingsText));
        ReferenceValidator.CheckComponentForNull(ref goBackButton, gameObject, nameof(goBackButton));
        ReferenceValidator.CheckComponentForNull(ref mainMusicSlider, gameObject, nameof(mainMusicSlider));
    }
}