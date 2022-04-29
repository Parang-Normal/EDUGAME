using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIActions : MonoBehaviour
{ 
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(GameMode.Instance.NextLevel);
    }

    public void LoadLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameMode.Instance.ResetTimer();
    }

    public void ToggleMusic()
    {
        bool Music = SystemManager.Instance.Music;

        if (Music)
            SystemManager.Instance.SetMusic(false);
        else
            SystemManager.Instance.SetMusic(true);
    }

    public void ToggleSFX()
    {
        bool SFX = SystemManager.Instance.SFX;

        if (SFX)
            SystemManager.Instance.SetSFX(false);
        else
            SystemManager.Instance.SetSFX(true);
    }


}
