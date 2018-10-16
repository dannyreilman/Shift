using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Timestamp))]
public class TimestampDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

		var barRect = new Rect(position.x, position.y, 30, position.height);
        var beatRect = new Rect(position.x + 35, position.y, 50, position.height);
        var subdivisionRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);
		
        EditorGUI.PropertyField(barRect, property.FindPropertyRelative("bars"), GUIContent.none);
        EditorGUI.PropertyField(beatRect, property.FindPropertyRelative("beats"), GUIContent.none);
    	EditorGUI.PropertyField(subdivisionRect, property.FindPropertyRelative("subdivision"), GUIContent.none);
		
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;
	
        EditorGUI.EndProperty();
	}
}
