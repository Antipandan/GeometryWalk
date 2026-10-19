using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleLerper : MonoBehaviour
{
    [SerializeField] [Tooltip("Time in milliseconds")] [Range(0f, 60000f)]
    private float lerpingTime = 15f;
    private float elapsedTime = 0f;

    public float LerpingTime
    {
        get => lerpingTime;
    }

    public float LerpingTimeSeconds
    {
        get => lerpingTime / 1000f;
    }
    
    public IEnumerator LerpClampRotation(GameObject lerpObject)
    {
        // ReSharper disable once PossibleLossOfFraction
        float currentRotationZ = lerpObject.transform.rotation.eulerAngles.z;
        float rounded = Mathf.Round(currentRotationZ / 45);
        float desiredRotation = (rounded + (45f * rounded % 2)) * 45;
        float startingRotation = lerpObject.transform.rotation.eulerAngles.z;
        while (elapsedTime <= LerpingTimeSeconds)
        {
            float step = elapsedTime / LerpingTimeSeconds;
            LerpRotation(lerpObject, startingRotation, desiredRotation, step);
            yield return null;
        }
        LerpRotation(lerpObject, startingRotation, desiredRotation, 1f);
        elapsedTime = 0f;
        yield break;
    }

    private void LerpRotation(GameObject lerpObject, float start, float end, float step = 0f)
    {
        float currentRotation = Mathf.Lerp(start, end, step);
        lerpObject.transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        elapsedTime += Time.deltaTime;        
    }
    
}