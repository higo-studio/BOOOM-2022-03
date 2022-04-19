using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TypeWriter : MonoBehaviour
{
    public bool IsPlaying = false;
    [Range(0f, 1f)]
    public float Progress;
    public string Text;
    public string OutputText;
    public float Speed = 1;

    private ChangedApplyListener<int> cursor;

    public bool IsStopped => Progress == 1f;

    public void Play(string text)
    {
        Progress = 0;
        IsPlaying = true;
        OutputText = null;
        Text = text;
    }

    public void Pause()
    {
        IsPlaying = false;
    }

    public void Resume()
    {
        IsPlaying = true;
    }

    public void Skip()
    {
        Progress = 1;
    }

    public virtual void UpdateText()
    {

    }

    private void Update()
    {
        cursor.Value = Mathf.FloorToInt(Progress * Text.Length);

        if (cursor.Update(out var val) && !string.IsNullOrEmpty(Text))
        {
            OutputText = Text.Substring(0, val);
            UpdateText();
        }
    }

    private void LateUpdate()
    {
        if (IsPlaying && !string.IsNullOrEmpty(Text))
        {
            Progress += Time.deltaTime / (1f / Speed) / Text.Length;
            Progress = Mathf.Clamp01(Progress);
        }
    }
}
