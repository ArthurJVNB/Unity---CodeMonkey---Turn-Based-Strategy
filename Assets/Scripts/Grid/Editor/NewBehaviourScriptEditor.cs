using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SW
{
	// Doc: https://docs.unity3d.com/ScriptReference/Editor.html

	// Please rewrite bellow the correct script name you want this Editor script to customize it's Inpector behaviour.
	[CustomEditor(typeof(NewBehaviourScriptEditor))]
	[CanEditMultipleObjects]
	public class NewBehaviourScriptEditor : Editor
	{
		// It automatically can handle editing, undo, and Prefab overrides
		private SerializedProperty _property;

		private void OnEnable()
		{
			// Example of how to obtain the desired property.
			_property = serializedObject.FindProperty("_propertyName");
		}

		public override void OnInspectorGUI()
		{
			// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
			serializedObject.Update();

			// Default GUI - Delete this line if you want to customize everything.
			base.OnInspectorGUI();

			// This is how you obtain the script and use its Methods and variables - for variables is better to use SerializedProperties.
			// YourScript script = (YourScript)target;

			// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
