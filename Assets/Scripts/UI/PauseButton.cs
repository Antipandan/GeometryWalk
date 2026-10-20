using System;
using CustomUtility;
using UnityEngine;
using UnityEngine.UI;


public class PauseButton : ButtonBase
{
    [SerializeField] private Image icon;
    [SerializeField] private PauseButtonImages images;
    private void Pause()
    {
        GameUtilities.SwitchPausePlay();
        if (images != null && icon != null)
        {
            icon.sprite = images.GetAppropriateImage(Time.timeScale);
        }
    }

    protected override void SubscribeEvents()
    {
        if (button == null) return;
        button.onClick.RemoveListener(Pause);
        button.onClick.AddListener(Pause);
        button.onClick.RemoveListener(EventBus<GamePausedEvent>.Raise);
        button.onClick.AddListener(EventBus<GamePausedEvent>.Raise);
    }

    protected override void UnsubscribeEvents()
    {
        if (button == null) return;
        button.onClick.RemoveListener(Pause);
        button.onClick.RemoveListener(EventBus<GamePausedEvent>.Raise);
    }

    protected override void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref button, gameObject, "Restart button");
        ReferenceValidator.CheckUnityObjectForNull(ref images, nameof(images));
    }
}