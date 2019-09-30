using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TimingManager.Window))]
public class TimingWindowDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		label.text = "D/P/C";
		EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

		var delayRect = new Rect(position.x, position.y, 30, position.height);
        var pointRect = new Rect(position.x + 35, position.y, 50, position.height);
        var comboRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);
		
        EditorGUI.PropertyField(delayRect, property.FindPropertyRelative("msDelay"), GUIContent.none);
        EditorGUI.PropertyField(pointRect, property.FindPropertyRelative("pointValue"), GUIContent.none);
    	EditorGUI.PropertyField(comboRect, property.FindPropertyRelative("breaksCombo"), GUIContent.none);
		
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;
	
        EditorGUI.EndProperty();
	}
}
