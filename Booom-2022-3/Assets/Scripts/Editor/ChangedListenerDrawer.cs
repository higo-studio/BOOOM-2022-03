using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ChangedApplyListener<>))]
public class ChangedApplyLisenterDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var caRangeAttriArr = fieldInfo.GetCustomAttributes(typeof(CARangeAttribute), true);
        if (caRangeAttriArr != null && caRangeAttriArr.Length > 0)
        {
            var attri = caRangeAttriArr[0] as CARangeAttribute;
            var valueProp = property.FindPropertyRelative("Value");
            EditorGUI.Slider(position, valueProp, attri.min, attri.max, label);
        }
        else
        {
            var valueProp = property.FindPropertyRelative("Value");
            EditorGUI.PropertyField(position, valueProp, label);
        }
    }
}