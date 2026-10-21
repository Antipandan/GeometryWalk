using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using CustomUtility;

public sealed class PauseMenu : MonoBehaviour
{
    // Just making sure these exist!
    [SerializeField] [Tooltip("GameObject that holds everything")]
    private GameObject holder;
    [SerializeField] [Tooltip("Background that should exist on the gameObject")] private Image background;
    [SerializeField] [Tooltip("Pause text that should exist on the gameObject")] private TextMeshProUGUI text;
    [SerializeField] [Tooltip("Restart button that should exist on the gameObject")] private ButtonBase restartButton;
    [SerializeField] [Tooltip("Resume button that should exist on the gameObject")] private ButtonBase resumeButton;
    [SerializeField] [Tooltip("Exit main menu button that should exist on the gameObject")] private ButtonBase exitMainMenuButton;
    private EventBinding<GamePausedEvent> gamePausedEvent;
    private EventBinding<GameResumedEvent> gameResumedEvent;
    private void Awake()
    {
        gamePausedEvent = new EventBinding<GamePausedEvent>(Pause);
        gameResumedEvent = new EventBinding<GameResumedEvent>(Resume);
        CheckReferences();
        SubscribeEvents();
        holder.SetActive(false);
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
        EventBus<GamePausedEvent>.Register(gamePausedEvent);
        EventBus<GameResumedEvent>.Register(gameResumedEvent);
    }

    private void UnsubscribeEvents()
    {
        EventBus<GamePausedEvent>.DeRegister(gamePausedEvent);
        EventBus<GameResumedEvent>.DeRegister(gameResumedEvent);
    }

    private void Pause()
    {
        holder.SetActive(true);
    }

    private void Resume()
    {
        holder.SetActive(false);
    }

    private void CheckReferences()
    {
        if (holder == null) ReferenceValidator.CheckUnityObjectForNull(ref holder, ErrorSeverity.FatalError, preventPlay: true);
        ReferenceValidator.CheckComponentForNull(ref background, gameObject, nameof(background));
        ReferenceValidator.CheckComponentForNull(ref text, gameObject, nameof(text));
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
        ReferenceValidator.CheckComponentForNull(ref resumeButton, gameObject, nameof(resumeButton));
        ReferenceValidator.CheckComponentForNull(ref exitMainMenuButton, gameObject, nameof(exitMainMenuButton));
    }
}