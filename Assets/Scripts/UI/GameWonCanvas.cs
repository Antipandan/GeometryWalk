using System;
using CustomUtility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameWonCanvas : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI GameWonText;
    [SerializeField] private ButtonBase restartButton;
    [SerializeField] private ButtonBase exitButton;
    private EventBinding<GameWonEvent> onGameWon;

    private void Awake()
    {
        CheckReferences();
        onGameWon = new EventBinding<GameWonEvent>(GameWon);
    }

    private void OnEnable()
    {
        EventBus<GameWonEvent>.Register(onGameWon);
    }

    private void OnDisable()
    {
        EventBus<GameWonEvent>.DeRegister(onGameWon);
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckUnityObjectForNull(ref holder, nameof(holder));
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
        ReferenceValidator.CheckComponentForNull(ref GameWonText, gameObject, nameof(GameWonText));
        ReferenceValidator.CheckComponentForNull(ref exitButton, gameObject, nameof(exitButton));
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
    }

    private void GameWon()
    {
        if (holder != null) holder.SetActive(true);
    }

    private void SubscribeEvents()
    {
        
    }

    private void UnsubscribeEvents()
    {
        
    }
}