using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DigitStats))]
public class DigitStatsDrawer : PropertyDrawer
{
    int lines = 4;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        float height = position.height / lines;

        SerializedProperty valueProperty = property.FindPropertyRelative("Value");
        SerializedProperty incrementProperty = property.FindPropertyRelative("Increment");
        SerializedProperty interactProperty = property.FindPropertyRelative("Interactable");

        Rect valueRect = new Rect(position.x, position.y + height, position.width, height);
        Rect incrementRect = new Rect(position.x, position.y + height * 2, position.width, height);
        Rect interactRect = new Rect(position.x, position.y + height * 3, position.width, height);

        EditorGUI.LabelField(valueRect, new GUIContent("Value"), new GUIContent(valueProperty.intValue.ToString()));
        EditorGUI.LabelField(incrementRect, new GUIContent("Increment"), new GUIContent(incrementProperty.intValue.ToString()));
        EditorGUI.LabelField(interactRect, new GUIContent("Interactable"), new GUIContent(interactProperty.boolValue.ToString()));

        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * lines;
    }
}
