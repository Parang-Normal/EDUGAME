using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnemyStats))]
public class EnemyStatsDrawer : PropertyDrawer
{
    int lines = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        float height = position.height / lines;

        SerializedProperty killableProperty = property.FindPropertyRelative("Killable");
        SerializedProperty healthProperty = property.FindPropertyRelative("Health");
        SerializedProperty ongroundProperty = property.FindPropertyRelative("OnGround");
        SerializedProperty rangeProperty = property.FindPropertyRelative("WalkRange");

        Rect killableRect = new Rect(position.x, position.y + height, position.width, height);
        Rect healthRect = new Rect(position.x, position.y + height * 2, position.width, height);
        Rect ongroundRect = new Rect(position.x, position.y + height * 3, position.width, height);
        Rect rangeRect = new Rect(position.x, position.y + height * 4, position.width, height);

        EditorGUI.PropertyField(killableRect, killableProperty, new GUIContent("Killable"));
        EditorGUI.LabelField(healthRect, new GUIContent("Health"), new GUIContent(healthProperty.intValue.ToString()));
        EditorGUI.LabelField(ongroundRect, new GUIContent("On Ground"), new GUIContent(ongroundProperty.boolValue.ToString()));
        EditorGUI.PropertyField(rangeRect, rangeProperty, new GUIContent("Walk Range"));

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * lines;
    }
}
