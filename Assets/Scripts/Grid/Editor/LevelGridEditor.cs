using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SW.Grid
{
	// Doc: https://docs.unity3d.com/ScriptReference/Editor.html

	[CustomEditor(typeof(LevelGrid))]
	[CanEditMultipleObjects]
	public class LevelGridEditor : Editor
	{
		private const float k_yAboveGround = 0;

		private bool _seeDebug = true;
		private bool _seeWireFrame = true;

		// It automatically handles editing, undo, and Prefab overrides
		private SerializedProperty _width;
		private SerializedProperty _height;
		private SerializedProperty _cellSize;

		//private GridSystem _grid;

		private void OnEnable()
		{
			// Example of how to obtain the desired property.
			_width = serializedObject.FindProperty("_width");
			_height = serializedObject.FindProperty("_height");
			_cellSize = serializedObject.FindProperty("_cellSize");
		}

		public override void OnInspectorGUI()
		{
			// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
			//serializedObject.Update();

			// Default GUI - Delete this line if you want to customize everything.
			base.OnInspectorGUI();
			
			DebugOptions();

			// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
			//serializedObject.ApplyModifiedProperties();
		}

		private void OnSceneGUI()
		{
			if (!_seeDebug) return;

			GUIStyle labelStyle = new()
			{
				alignment = TextAnchor.MiddleCenter,
				fontStyle = FontStyle.Normal,
			};
			labelStyle.normal.textColor = Color.white;

			Vector3 position;
			for (int x = 0; x < _width.intValue; x++)
			{
				for (int z = 0; z < _height.intValue; z++)
				{
					position = new(x * _cellSize.intValue, k_yAboveGround, z * _cellSize.intValue);
					Handles.Label(position, $"{x}, {z}", labelStyle);

					if (_seeWireFrame)
					{
						//Handles.DrawWireCube(position, new(_cellSize.intValue, .01f, _cellSize.intValue));
						Vector3 p1 = new(position.x - _cellSize.intValue / 2f, k_yAboveGround, position.z - _cellSize.intValue / 2f);
						Vector3 p2 = new(position.x + _cellSize.intValue / 2f, k_yAboveGround, position.z - _cellSize.intValue / 2f);
						Vector3 p3 = new(position.x + _cellSize.intValue / 2f, k_yAboveGround, position.z + _cellSize.intValue / 2f);
						Vector3 p4 = new(position.x - _cellSize.intValue / 2f, k_yAboveGround, position.z + _cellSize.intValue / 2f);
						Handles.DrawLine(p1, p2);
						Handles.DrawLine(p2, p3);
						Handles.DrawLine(p3, p4);
						Handles.DrawLine(p4, p1);
					}
				}
			}
		}

		private void DebugOptions()
		{
			EditorGUI.BeginChangeCheck();

			GUILayout.Space(8);
			GUILayout.Label("DEBUG OPTIONS");

			bool guiEnabled = GUI.enabled;
			_seeDebug = GUILayout.Toggle(_seeDebug, "See Debug");

			if (_seeDebug)
				_seeWireFrame = GUILayout.Toggle(_seeWireFrame, "See Wire Frame");

			if (EditorGUI.EndChangeCheck())
				SceneView.RepaintAll();
		}
	}
}
