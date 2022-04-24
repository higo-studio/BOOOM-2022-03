using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void RestartGame()
    {
        SceneFader.instance.FadeTo("TestScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BackToMenu()
    {
        SceneFader.instance.FadeTo("TitleMenu");
    }

    public void PauseGame()
    {
        transform.Find("Setting").Find("SettingP").gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        transform.Find("Setting").Find("SettingP").gameObject.SetActive(false);
    }


}
