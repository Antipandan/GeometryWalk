using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Pause Button Images", fileName = "PauseButtonImages")]
public class PauseButtonImages : ScriptableObject
{
    [SerializeField] private Sprite pauseImage;
    [SerializeField] private Sprite playImage;

    public Sprite PauseImage
    {
        get => pauseImage;
    }

    public Sprite PlayImage
    {
        get => playImage;
    }

    public Sprite GetAppropriateImage(float timeScaleValue)
    {
        return timeScaleValue == 0f ? pauseImage : playImage;
    }
}