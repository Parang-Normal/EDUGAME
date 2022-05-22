using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ProblemStats))]
public class ProblemStatsDrawer : PropertyDrawer
{
    int lines = 6;
    int offset = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        float height = position.height / lines;

        SerializedProperty probProperty = property.FindPropertyRelative("Problem");
        SerializedProperty opProperty = property.FindPropertyRelative("Operation");
        SerializedProperty minProperty;
        SerializedProperty maxProperty;
        SerializedProperty valueProperty;
        
        Rect digitRect;
        Rect NewPos;
        Rect minRectLabel;
        Rect minRect;
        Rect maxRectLabel;
        Rect maxRect;
        Rect valueRectLabel;
        Rect valueRect;

        //Problem
        Rect probRect = new Rect(position.x, position.y + height * 1, position.width, height);
        EditorGUI.PropertyField(probRect, probProperty, new GUIContent("Problem"));

        //Operation
        Rect opRect = new Rect(position.x, position.y + height * 2, position.width, height);
        EditorGUI.PropertyField(opRect, opProperty, new GUIContent("Operation"));

        int indent = EditorGUI.indentLevel;

        //First Digit
        minProperty = property.FindPropertyRelative("FirstDigit_MinValue");
        maxProperty = property.FindPropertyRelative("FirstDigit_MaxValue");
        valueProperty = property.FindPropertyRelative("FirstDigit_Value");

        offset = 3;
        digitRect = new Rect(position.x, position.y + height * offset, position.width, height);
        NewPos = EditorGUI.PrefixLabel(digitRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("First Digit"));
       
        EditorGUI.indentLevel = 0;

        minRectLabel = new Rect(NewPos.x, position.y + height * offset, NewPos.width * 0.1f, height);
        minRect = new Rect(NewPos.x + NewPos.width * 0.15f, position.y + height * offset, NewPos.width * 0.15f, height);
        maxRectLabel = new Rect(NewPos.x + NewPos.width * 0.35f, position.y + height * offset, NewPos.width * 0.1f, height);
        maxRect = new Rect(NewPos.x + NewPos.width * 0.5f, position.y + height * offset, NewPos.width * 0.15f, height);
        valueRectLabel = new Rect(NewPos.x + NewPos.width * 0.7f, position.y + height * offset, NewPos.width * 0.1f, height);
        valueRect = new Rect(NewPos.x + NewPos.width * 0.85f, position.y + height * offset, NewPos.width * 0.15f, height);

        EditorGUI.PrefixLabel(minRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Min"));
        if (probProperty.intValue != (int)MissingNumber.FirstDigit)
            EditorGUI.PropertyField(minRect, minProperty, GUIContent.none);
        else
            EditorGUI.LabelField(minRect, GUIContent.none, new GUIContent(minProperty.intValue.ToString()));

        EditorGUI.PrefixLabel(maxRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Max"));
        if (probProperty.intValue != (int)MissingNumber.FirstDigit)
            EditorGUI.PropertyField(maxRect, maxProperty, GUIContent.none);
        else
            EditorGUI.LabelField(maxRect, GUIContent.none, new GUIContent(maxProperty.intValue.ToString()));

        EditorGUI.PrefixLabel(valueRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Value"));
        EditorGUI.LabelField(valueRect, GUIContent.none, new GUIContent(valueProperty.intValue.ToString()));
        //End of First Digit

        //Second Digit
        minProperty = property.FindPropertyRelative("SecondDigit_MinValue");
        maxProperty = property.FindPropertyRelative("SecondDigit_MaxValue");
        valueProperty = property.FindPropertyRelative("SecondDigit_Value");

        offset = 4;
        digitRect = new Rect(position.x, position.y + height * offset, position.width, height);
        NewPos = EditorGUI.PrefixLabel(digitRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Second Digit"));

        EditorGUI.indentLevel = 0;

        minRectLabel = new Rect(NewPos.x, position.y + height * offset, NewPos.width * 0.1f, height);
        minRect = new Rect(NewPos.x + NewPos.width * 0.15f, position.y + height * offset, NewPos.width * 0.15f, height);
        maxRectLabel = new Rect(NewPos.x + NewPos.width * 0.35f, position.y + height * offset, NewPos.width * 0.1f, height);
        maxRect = new Rect(NewPos.x + NewPos.width * 0.5f, position.y + height * offset, NewPos.width * 0.15f, height);
        valueRectLabel = new Rect(NewPos.x + NewPos.width * 0.7f, position.y + height * offset, NewPos.width * 0.1f, height);
        valueRect = new Rect(NewPos.x + NewPos.width * 0.85f, position.y + height * offset, NewPos.width * 0.15f, height);

        EditorGUI.PrefixLabel(minRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Min"));
        if (probProperty.intValue != (int)MissingNumber.SecondDigit)
            EditorGUI.PropertyField(minRect, minProperty, GUIContent.none);
        else
            EditorGUI.LabelField(minRect, GUIContent.none, new GUIContent(minProperty.intValue.ToString()));

        EditorGUI.PrefixLabel(maxRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Max"));
        if (probProperty.intValue != (int)MissingNumber.SecondDigit)
            EditorGUI.PropertyField(maxRect, maxProperty, GUIContent.none);
        else
            EditorGUI.LabelField(maxRect, GUIContent.none, new GUIContent(maxProperty.intValue.ToString()));

        EditorGUI.PrefixLabel(valueRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Value"));
        EditorGUI.LabelField(valueRect, GUIContent.none, new GUIContent(valueProperty.intValue.ToString()));
        //End of Second Digit

        //Result
        minProperty = property.FindPropertyRelative("Result_MinValue");
        maxProperty = property.FindPropertyRelative("Result_MaxValue");
        valueProperty = property.FindPropertyRelative("Result_Value");

        offset = 5;
        digitRect = new Rect(position.x, position.y + height * offset, position.width, height);
        NewPos = EditorGUI.PrefixLabel(digitRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Result"));

        EditorGUI.indentLevel = 0;

        minRectLabel = new Rect(NewPos.x, position.y + height * offset, NewPos.width * 0.1f, height);
        minRect = new Rect(NewPos.x + NewPos.width * 0.15f, position.y + height * offset, NewPos.width * 0.15f, height);
        maxRectLabel = new Rect(NewPos.x + NewPos.width * 0.35f, position.y + height * offset, NewPos.width * 0.1f, height);
        maxRect = new Rect(NewPos.x + NewPos.width * 0.5f, position.y + height * offset, NewPos.width * 0.15f, height);
        valueRectLabel = new Rect(NewPos.x + NewPos.width * 0.7f, position.y + height * offset, NewPos.width * 0.1f, height);
        valueRect = new Rect(NewPos.x + NewPos.width * 0.85f, position.y + height * offset, NewPos.width * 0.15f, height);

        EditorGUI.PrefixLabel(minRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Min"));
        //if (probProperty.intValue == (int)MissingNumber.Result)
            EditorGUI.PropertyField(minRect, minProperty, GUIContent.none);
        //else
        //    EditorGUI.LabelField(minRect, GUIContent.none, new GUIContent(minProperty.intValue.ToString()));

        EditorGUI.PrefixLabel(maxRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Max"));
        //if (probProperty.intValue == (int)MissingNumber.Result)
            EditorGUI.PropertyField(maxRect, maxProperty, GUIContent.none);
        //else
        //    EditorGUI.LabelField(maxRect, GUIContent.none, new GUIContent(maxProperty.intValue.ToString()));

        EditorGUI.PrefixLabel(valueRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Value"));
        EditorGUI.LabelField(valueRect, GUIContent.none, new GUIContent(valueProperty.intValue.ToString()));
        //End of Result

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * lines;
    }
}
