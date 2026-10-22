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
    [SerializeField] private MainMusicSoundPlayer mainMusicPlayer;

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
        if (slider == null || mainMusicPlayer == null) return;
        slider.onValueChanged.RemoveListener(ChangeVolume);
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void UnsubscribeEvents()
    {
        if (slider == null || mainMusicPlayer == null) return;
        slider.onValueChanged.RemoveListener(ChangeVolume);
    }
    
    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref musicText, gameObject, nameof(musicText));
        ReferenceValidator.CheckComponentForNull(ref slider, gameObject, nameof(slider));
        if (mainMusicPlayer == null) mainMusicPlayer = FindAnyObjectByType<MainMusicSoundPlayer>();
    }
    
    // wrapper so we can change the saved music volume settings
    private void ChangeVolume(float volume)
    {
        if (mainMusicPlayer != null) mainMusicPlayer.ChangeVolume(volume);
        else mainMusicPlayer?.ChangeVolume(volume);
        

        SetSavedMusicVolume();
    }
    
    private void SetSavedMusicVolume()
    {
        if (musicSettings != null && mainMusicPlayer != null)
        {
            musicSettings.Volume = mainMusicPlayer.Volume;
        }
    }

    private void ApplyMusicVolume()
    {
        if (musicSettings != null && mainMusicPlayer != null)
        {
            mainMusicPlayer.ChangeVolume(musicSettings.Volume);
            if (slider != null) slider.value = mainMusicPlayer.Volume;
        }
    }
    
}