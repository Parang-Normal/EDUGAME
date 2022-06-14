using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ProblemStats))]
public class ProblemStatsDrawer : PropertyDrawer
{
    int lines = 9;
    int offset = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        float height = position.height / lines;

        SerializedProperty probProperty = property.FindPropertyRelative("Problem");
        SerializedProperty opProperty = property.FindPropertyRelative("Operation");
        SerializedProperty useDivProperty = property.FindPropertyRelative("UseDivisibility");
        SerializedProperty divProperty = property.FindPropertyRelative("Divisor");
        SerializedProperty incrementProperty = property.FindPropertyRelative("Increment");
        SerializedProperty minProperty, maxProperty;
        
        Rect digitRect, NewPos;
        Rect minRectLabel, minRect;
        Rect maxRectLabel, maxRect;
        Rect incrementRectLabel, valuesLabel;
        Rect divRectLabel, divRect;
        Rect useDivRectLabel, useDivRect;

        GUIContent minContent = new GUIContent();
        minContent.text = "Min";
        minContent.tooltip = "Min value to randomize";

        GUIContent maxContent = new GUIContent();
        maxContent.text = "Max";
        maxContent.tooltip = "Max value to randomize";

        GUIContent incrementContent = new GUIContent();
        incrementContent.text = "Increment";
        incrementContent.tooltip = "Value to increment";

        GUIContent divContent = new GUIContent();
        divContent.text = "Div";
        divContent.tooltip = "Divisor";

        //Problem
        Rect probRect = new Rect(position.x, position.y + height * 1, position.width, height);
        EditorGUI.PropertyField(probRect, probProperty, new GUIContent("Problem"));

        //Operation
        Rect opRect = new Rect(position.x, position.y + height * 2, position.width, height);
        EditorGUI.PropertyField(opRect, opProperty, new GUIContent("Operation"));

        int indent = EditorGUI.indentLevel;

        //Values label
        offset = 3;
        valuesLabel = new Rect(position.x, position.y + height * offset, position.width, height);
        EditorGUI.PrefixLabel(valuesLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Values"));

        //Increment
        offset++;
        incrementRectLabel = new Rect(position.x, position.y + height * offset, position.width, height);
        NewPos = EditorGUI.PrefixLabel(incrementRectLabel, GUIUtility.GetControlID(FocusType.Passive), incrementContent);
        EditorGUI.PropertyField(NewPos, incrementProperty, GUIContent.none);

        //First Digit
        minProperty = property.FindPropertyRelative("FirstDigit_MinValue");
        maxProperty = property.FindPropertyRelative("FirstDigit_MaxValue");

        offset++;
        digitRect = new Rect(position.x, position.y + height * offset, position.width, height);
        NewPos = EditorGUI.PrefixLabel(digitRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("First Digit"));
       
        EditorGUI.indentLevel = 0;

        minRectLabel = new Rect(NewPos.x, position.y + height * offset, NewPos.width * 0.1f, height);
        minRect = new Rect(NewPos.x + NewPos.width * 0.15f, position.y + height * offset, NewPos.width * 0.15f, height);
        maxRectLabel = new Rect(NewPos.x + NewPos.width * 0.5f, position.y + height * offset, NewPos.width * 0.1f, height);
        maxRect = new Rect(NewPos.x + NewPos.width * 0.65f, position.y + height * offset, NewPos.width * 0.15f, height);

        EditorGUI.PrefixLabel(minRectLabel, GUIUtility.GetControlID(FocusType.Passive), minContent);
        if (probProperty.intValue != (int)MissingNumber.FirstDigit)
            EditorGUI.PropertyField(minRect, minProperty, GUIContent.none);
        else
            EditorGUI.LabelField(minRect, GUIContent.none, new GUIContent(minProperty.intValue.ToString()));

        EditorGUI.PrefixLabel(maxRectLabel, GUIUtility.GetControlID(FocusType.Passive), maxContent);
        if (probProperty.intValue != (int)MissingNumber.FirstDigit)
            EditorGUI.PropertyField(maxRect, maxProperty, GUIContent.none);
        else
            EditorGUI.LabelField(maxRect, GUIContent.none, new GUIContent(maxProperty.intValue.ToString()));
        //End of First Digit

        //Second Digit
        minProperty = property.FindPropertyRelative("SecondDigit_MinValue");
        maxProperty = property.FindPropertyRelative("SecondDigit_MaxValue");

        offset++;
        digitRect = new Rect(position.x, position.y + height * offset, position.width, height);
        NewPos = EditorGUI.PrefixLabel(digitRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Second Digit"));

        EditorGUI.indentLevel = 0;

        minRectLabel = new Rect(NewPos.x, position.y + height * offset, NewPos.width * 0.1f, height);
        minRect = new Rect(NewPos.x + NewPos.width * 0.15f, position.y + height * offset, NewPos.width * 0.15f, height);
        maxRectLabel = new Rect(NewPos.x + NewPos.width * 0.5f, position.y + height * offset, NewPos.width * 0.1f, height);
        maxRect = new Rect(NewPos.x + NewPos.width * 0.65f, position.y + height * offset, NewPos.width * 0.15f, height);

        EditorGUI.PrefixLabel(minRectLabel, GUIUtility.GetControlID(FocusType.Passive), minContent);
        if (probProperty.intValue != (int)MissingNumber.SecondDigit)
            EditorGUI.PropertyField(minRect, minProperty, GUIContent.none);
        else
            EditorGUI.LabelField(minRect, GUIContent.none, new GUIContent(minProperty.intValue.ToString()));

        EditorGUI.PrefixLabel(maxRectLabel, GUIUtility.GetControlID(FocusType.Passive), maxContent);
        if (probProperty.intValue != (int)MissingNumber.SecondDigit)
            EditorGUI.PropertyField(maxRect, maxProperty, GUIContent.none);
        else
            EditorGUI.LabelField(maxRect, GUIContent.none, new GUIContent(maxProperty.intValue.ToString()));
        //End of Second Digit

        //Result
        minProperty = property.FindPropertyRelative("Result_MinValue");
        maxProperty = property.FindPropertyRelative("Result_MaxValue");

        offset++;
        digitRect = new Rect(position.x, position.y + height * offset, position.width, height);
        NewPos = EditorGUI.PrefixLabel(digitRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Result"));

        EditorGUI.indentLevel = 0;

        minRectLabel = new Rect(NewPos.x, position.y + height * offset, NewPos.width * 0.1f, height);
        minRect = new Rect(NewPos.x + NewPos.width * 0.15f, position.y + height * offset, NewPos.width * 0.15f, height);
        maxRectLabel = new Rect(NewPos.x + NewPos.width * 0.5f, position.y + height * offset, NewPos.width * 0.1f, height);
        maxRect = new Rect(NewPos.x + NewPos.width * 0.65f, position.y + height * offset, NewPos.width * 0.15f, height);

        EditorGUI.PrefixLabel(minRectLabel, GUIUtility.GetControlID(FocusType.Passive), minContent);
        EditorGUI.PropertyField(minRect, minProperty, GUIContent.none);

        EditorGUI.PrefixLabel(maxRectLabel, GUIUtility.GetControlID(FocusType.Passive), maxContent);
        EditorGUI.PropertyField(maxRect, maxProperty, GUIContent.none);
        //End of Result

        //Divisor
        offset++;
        digitRect = new Rect(position.x, position.y + height * offset, position.width, height);
        NewPos = EditorGUI.PrefixLabel(digitRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Divisbility"));

        EditorGUI.indentLevel = 0;

        useDivRectLabel = new Rect(NewPos.x, position.y + height * offset, NewPos.width * 0.1f, height);
        useDivRect = new Rect(NewPos.x + NewPos.width * 0.15f, position.y + height * offset, NewPos.width * 0.15f, height);
        divRectLabel = new Rect(NewPos.x + NewPos.width * 0.5f, position.y + height * offset, NewPos.width * 0.1f, height);
        divRect = new Rect(NewPos.x + NewPos.width * 0.65f, position.y + height * offset, NewPos.width * 0.15f, height);

        EditorGUI.PrefixLabel(useDivRectLabel, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Use"));
        EditorGUI.PropertyField(useDivRect, useDivProperty, GUIContent.none);

        if(useDivProperty.boolValue == true)
        {
            EditorGUI.PrefixLabel(divRectLabel, GUIUtility.GetControlID(FocusType.Passive), divContent);
            EditorGUI.PropertyField(divRect, divProperty, GUIContent.none);
        }
        //End of Divisor

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * lines;
    }
}
