using System;
using CustomUtility;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : ButtonBase
{
    private static void Restart()
    {
        GameUtilities.ReloadScene();
        GameUtilities.ChangeTimeValue(1f);
    }

    protected override void SubscribeEvents()
    {
        button.onClick.RemoveListener(Restart);
        button.onClick.AddListener(Restart);
    }

    protected override void UnsubscribeEvents()
    {
        button.onClick.RemoveListener(Restart);
    }
}