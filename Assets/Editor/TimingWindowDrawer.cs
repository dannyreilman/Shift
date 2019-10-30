using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TimingManager.Window))]
public class TimingWindowDrawer : PropertyDrawer
{
    const float fullWidth = 600.0f;
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
        if(position.width > fullWidth)
        {
		    label.text = "Name / Delay / Points / Breaks / Color";
        }
        else
        {
		    label.text = "N / D / P / B / C";
        }
		EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

		var nameRect = new Rect(position.x, position.y, 60, position.height);
		var delayRect = new Rect(position.x + 65, position.y, 30, position.height);
        var pointRect = new Rect(position.x + 100, position.y, 30, position.height);
        var comboRect = new Rect(position.x + 135, position.y, 15, position.height);
		var colorRect = new Rect(position.x + 155, position.y, position.width - 155, position.height);
		
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
        EditorGUI.PropertyField(delayRect, property.FindPropertyRelative("msDelay"), GUIContent.none);
        EditorGUI.PropertyField(pointRect, property.FindPropertyRelative("pointValue"), GUIContent.none);
    	EditorGUI.PropertyField(comboRect, property.FindPropertyRelative("breaksCombo"), GUIContent.none);
        EditorGUI.PropertyField(colorRect, property.FindPropertyRelative("color"), GUIContent.none);
		
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;
	
        EditorGUI.EndProperty();
	}
}
