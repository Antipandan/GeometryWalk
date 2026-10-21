using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public static class GameUtilities
{
    public static void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);    
    }

    public static void LoadScene(string sceneName, bool tryConvert = false)
    {
        if (tryConvert) TryLoadSceneString(sceneName);
        else SceneManager.LoadScene(sceneName);
        return;
        
        static void TryLoadSceneString(string sceneName)
        {
            if (int.TryParse(sceneName, out int sceneIndex))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }

    public static void PauseGame()
    {
        ChangeTimeValue(0);
    }

    public static void ResumeGame()
    {
        ChangeTimeValue(1f);
    }

    public static void SwitchPausePlay()
    {
        float value = Convert.ToByte(Time.timeScale >= 1f ^ true);
        ChangeTimeValue(value);
    }
    
    public static void ChangeTimeValue(float time = 1f)
    {
        Time.timeScale = time;
    }

    public static float ClampVolume(float volume)
    {
        return Mathf.Clamp(volume, 0f, 1f);
    }

    public static float ClampPitch(float pitch)
    {
        return Mathf.Clamp(pitch, -3f, 3f);
    }
}