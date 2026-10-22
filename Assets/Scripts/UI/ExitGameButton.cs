using System;
using UnityEngine;
using UnityEngine.UI;
using CustomUtility;
public class ExitGameButton : MonoBehaviour
{
    [SerializeField] private Button exitGameButton;

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
        ReferenceValidator.CheckComponentForNull(ref exitGameButton, gameObject, nameof(exitGameButton));
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
        ErrorHandling.ExitPlayModeProperly();
        #endif
        Application.Quit();
    }

    private void SubscribeEvents()
    {
        if (exitGameButton == null) return;
        exitGameButton.onClick.RemoveListener(ExitGame);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void UnsubscribeEvents()
    {
        exitGameButton.onClick.RemoveListener(ExitGame);
    }
}