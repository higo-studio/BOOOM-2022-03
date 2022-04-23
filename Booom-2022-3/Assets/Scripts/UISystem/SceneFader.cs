using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;
    CanvasGroup canvasGroup;
    public float fadeTime;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        instance = FindObjectOfType<SceneFader>();
        StartCoroutine(FadeIn(fadeTime));
    }

    public void FadeTo(string _sceneName)
    {
        StartCoroutine(FadeOutIn(_sceneName, fadeTime));
    }

    IEnumerator FadeOutIn(string sceneName, float time)
    {
        yield return FadeOut(sceneName, time);
        yield return FadeIn(time);

    }

    IEnumerator FadeIn(float time)
    {
        canvasGroup.alpha = 1;
        while (canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }
    IEnumerator FadeOut(string sceneName, float time)
    {
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }

        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }

    }

}
