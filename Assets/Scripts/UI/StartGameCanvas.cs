using System;
using CustomUtility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI selectLevelText;
    [SerializeField] private Button returnButton;

    private void Awake()
    {
        CheckReferences();
        holder.SetActive(false);
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckUnityObjectForNull(ref holder, nameof(holder));
        ReferenceValidator.CheckComponentForNull(ref backgroundImage, gameObject, nameof(backgroundImage));
        ReferenceValidator.CheckComponentForNull(ref selectLevelText, gameObject, nameof(selectLevelText));
        ReferenceValidator.CheckComponentForNull(ref returnButton, gameObject, nameof(returnButton));
    }
}