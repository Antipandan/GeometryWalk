using System;
using CustomUtility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class MusicSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Slider slider;
    [SerializeField] private MusicSettings musicSettings;

    private void Awake()
    {
        CheckReferences();
    }

    private void OnEnable()
    {
        SubscribeEvents();
        ApplyMusicVolume();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        // singleton because it is easier than Events / eventbus
        if (slider == null || MainMusicSoundPlayer.Instance == null) return;
        slider.onValueChanged.RemoveListener(ChangeVolume);
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void UnsubscribeEvents()
    {
        if (slider == null || MainMusicSoundPlayer.Instance == null) return;
        slider.onValueChanged.RemoveListener(MainMusicSoundPlayer.Instance.ChangeVolume);
    }
    
    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref musicText, gameObject, nameof(musicText));
        ReferenceValidator.CheckComponentForNull(ref slider, gameObject, nameof(slider));
    }
    
    // wrapper so we can change the saved music volume settings
    private void ChangeVolume(float volume)
    {
        MainMusicSoundPlayer.Instance!.ChangeVolume(volume);
        SetSavedMusicVolume();
    }
    
    private void SetSavedMusicVolume()
    {
        if (musicSettings != null && MainMusicSoundPlayer.Instance != null)
        {
            musicSettings.Volume = MainMusicSoundPlayer.Instance.Volume;
        }
    }

    private void ApplyMusicVolume()
    {
        if (musicSettings != null && MainMusicSoundPlayer.Instance != null)
        {
            MainMusicSoundPlayer.Instance.ChangeVolume(musicSettings.Volume);
            if (slider != null) slider.value = MainMusicSoundPlayer.Instance.Volume;
        }
    }
    
}