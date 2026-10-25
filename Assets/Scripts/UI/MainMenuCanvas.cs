using System;
using CustomUtility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject itemHolder;
    [SerializeField] private TextMeshProUGUI gameText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        CheckReferences();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckUnityObjectForNull(ref itemHolder, nameof(itemHolder));
        ReferenceValidator.CheckComponentForNull(ref  gameText, gameObject, nameof(gameText));
        ReferenceValidator.CheckComponentForNull(ref playButton, gameObject, nameof(playButton));
        ReferenceValidator.CheckComponentForNull(ref settingsButton, gameObject, nameof(settingsButton));
        ReferenceValidator.CheckComponentForNull(ref quitButton, gameObject, nameof(quitButton));
    }

    private void SubscribeEvents()
    {
        
    }

    private void UnsubscribeEvents()
    {
        
    }
}