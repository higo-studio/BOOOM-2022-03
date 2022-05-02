using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[Serializable]
public struct ChangedApplyListener<T> where T : IComparable<T>
{
    public T Value;
    public T PreviousValue { get; private set; }

    private bool isInit;

    public bool Update(out T val)
    {
        val = default;
        if (!PreviousValue.Equals(Value) || !isInit)
        {
            val = Value;
            PreviousValue = Value;
            isInit = true;
            return true;
        }
        return false;
    }

    public static implicit operator T(ChangedApplyListener<T> src)
    {
        src.Update(out _);
        return src.Value;
    }

    public static implicit operator ChangedApplyListener<T>(T src)
    {
        ChangedApplyListener<T> news = default;
        news.Value = src;
        return news;
    }
}

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public sealed class CARangeAttribute : PropertyAttribute
{
    public readonly float min;

    public readonly float max;
    public CARangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

public class GlobalAudioSettings : MonoBehaviour
{
    [CARange(0f, 1f)]
    public ChangedApplyListener<float> Volumn;
    public ChangedApplyListener<bool> Paused;

    // Update is called once per frame
    void Update()
    {
        if (Volumn.Update(out var val))
        {
            AudioListener.volume = val;
        }

        if (Paused.Update(out var pval))
        {
            AudioListener.pause = pval;
        }
    }
}