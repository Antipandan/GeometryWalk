using System;
using CustomUtility;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public sealed class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string levelName;

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
        ReferenceValidator.CheckComponentForNull(ref button, gameObject, nameof(button));
        ReferenceValidator.CheckObjectForNull(ref levelName, gameObject, nameof(levelName));
    }

    private void LoadLevel()
    {
        GameUtilities.LoadScene(levelName, tryConvert: true);
    }

    private void SubscribeEvents()
    {
        if (button != null) button.onClick.AddListener(LoadLevel);
    }

    private void UnsubscribeEvents()
    {
        
    }
    
}