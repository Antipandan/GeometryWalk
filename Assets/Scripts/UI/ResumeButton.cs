using UnityEngine;
using System;
using UnityEngine.UI;

public class ResumeButton : ButtonBase
{
    protected override void SubscribeEvents()
    {
        if (button == null) return;
        button.onClick.RemoveListener(GameUtilities.ResumeGame);
        button.onClick.AddListener(GameUtilities.ResumeGame);
        button.onClick.RemoveListener(EventBus<GameResumedEvent>.Raise);
        button.onClick.AddListener(EventBus<GameResumedEvent>.Raise);
    }

    protected override void UnsubscribeEvents()
    {
        if (button == null) return;
        button.onClick.RemoveListener(GameUtilities.ResumeGame);
    }
}