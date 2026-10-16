using System;
using CustomUtility;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button restartButton;

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
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
    }

    private static void Restart()
    {
        GameUtilities.ReloadScene();
        GameUtilities.ChangeTimeValue(1f);
    }

    private void SubscribeEvents()
    {
        restartButton.onClick.RemoveListener(Restart);
        restartButton.onClick.AddListener(Restart);
    }

    private void UnsubscribeEvents()
    {
        restartButton.onClick.RemoveListener(Restart);
    }
}