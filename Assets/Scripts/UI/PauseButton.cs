using System;
using CustomUtility;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Image icon;
    [SerializeField] private PauseButtonImages images;

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
        UnSubscribeEvents();
    }

    private void Pause()
    {
        GameUtilities.SwitchPausePlay();
        if (images != null && icon != null)
        {
            icon.sprite = images.GetAppropriateImage(Time.timeScale);
        }
    }

    private void SubscribeEvents()
    {
        if (restartButton == null) return;
        restartButton.onClick.RemoveListener(Pause);
        restartButton.onClick.AddListener(Pause);
    }

    private void UnSubscribeEvents()
    {
        if (restartButton == null) return;
        restartButton.onClick.RemoveListener(Pause);
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
        ReferenceValidator.CheckUnityObjectForNull(ref images, nameof(images));
    }
}