using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Score : MonoBehaviour
{
    public Text text;

    int CurrentScore = 0;
    int DisplayScore = 0;

    public static Score instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        text.text = DisplayScore.ToString();
    }

    public void AddScore(int s)
    {
        CurrentScore += s;
        Tween tween = DOTween.To(() => DisplayScore, x => DisplayScore = x, CurrentScore, 1);
    }
}
