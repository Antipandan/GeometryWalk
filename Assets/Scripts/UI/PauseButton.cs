using System;
using CustomUtility;
using UnityEngine;
using UnityEngine.UI;


public sealed class PauseButton : ButtonBase
{
    [SerializeField] private Image icon;
    [SerializeField] private PauseButtonImages images;
    private EventBinding<GamePausedEvent> gamePausedEventBinding;
    private EventBinding<GameResumedEvent> gameResumedEventBinding;

    protected override void Awake()
    {
        base.Awake();
        gamePausedEventBinding = new EventBinding<GamePausedEvent>(AssignAppropriateSprite);
        gameResumedEventBinding = new EventBinding<GameResumedEvent>(AssignAppropriateSprite);
    }

    private void OnDestroy()
    {
        EventBus<GamePausedEvent>.DeRegister(gamePausedEventBinding);
        EventBus<GameResumedEvent>.DeRegister(gameResumedEventBinding);
    }

    private void Pause()
    {
        GameUtilities.SwitchPausePlay();
        if (images != null && icon != null)
        {
            AssignAppropriateSprite();
            if (Time.timeScale == 0f) EventBus<GamePausedEvent>.Raise();
            else EventBus<GameResumedEvent>.Raise();
        }
    }

    private void AssignAppropriateSprite()
    {
        icon.sprite = GetAppropriateSprite();
    }
    
    private Sprite GetAppropriateSprite()
    {
        return images.GetAppropriateImage(Time.timeScale);
    }

    protected override void SubscribeEvents()
    {
        if (button == null) return;
        button.onClick.RemoveListener(Pause);
        button.onClick.AddListener(Pause);
        EventBus<GamePausedEvent>.Register(gamePausedEventBinding);
        EventBus<GameResumedEvent>.Register(gameResumedEventBinding);
    }

    protected override void UnsubscribeEvents()
    {
        if (button == null) return;
        button.onClick.RemoveListener(Pause);
    }

    protected override void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref button, gameObject, "Restart button");
        ReferenceValidator.CheckUnityObjectForNull(ref images, nameof(images));
    }
}