using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using CustomUtility;

public sealed class PauseMenu : MonoBehaviour
{
    // Just making sure these exist!
    [SerializeField] [Tooltip("Background that should exist on the gameObject")] private Image background;
    [SerializeField] [Tooltip("Pause text that should exist on the gameObject")] private TextMeshProUGUI text;
    [SerializeField] [Tooltip("Restart button that should exist on the gameObject")] private ButtonBase restartButton;
    [SerializeField] [Tooltip("Resume button that should exist on the gameObject")] private ButtonBase resumeButton;
    [SerializeField] [Tooltip("Exit main menu button that should exist on the gameObject")] private ButtonBase exitMainMenuButton;

    private void Awake()
    {
        CheckReferences();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventBus<GamePausedEvent>.Register(new EventBinding<GamePausedEvent>(Pause));
        EventBus<GameResumedEvent>.Register(new EventBinding<GameResumedEvent>(Resume));
    }

    private void UnsubscribeEvents()
    {
        EventBus<GamePausedEvent>.DeRegister(new EventBinding<GamePausedEvent>(Pause));
        EventBus<GameResumedEvent>.DeRegister(new EventBinding<GameResumedEvent>(Resume));
    }

    private void Pause()
    {
        gameObject.SetActive(true);
    }

    private void Resume()
    {
        gameObject.SetActive(false);
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref background, gameObject, nameof(background));
        ReferenceValidator.CheckComponentForNull(ref text, gameObject, nameof(text));
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
        ReferenceValidator.CheckComponentForNull(ref resumeButton, gameObject, nameof(resumeButton));
        ReferenceValidator.CheckComponentForNull(ref exitMainMenuButton, gameObject, nameof(exitMainMenuButton));
    }
}