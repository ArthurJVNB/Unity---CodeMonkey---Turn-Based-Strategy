using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SW.Grid
{
	public class GridDebugObject : MonoBehaviour
	{
		[SerializeField] private TextMeshPro _text;

		private GridObject _gridObject;

		public GridObject GridObject
		{
			get => _gridObject;
			set
			{
				UnsubscribeGridObjectEvents();
				_gridObject = value;
				SubscribeGridObjectEvents();
				UpdateText();
			}
		}

		private void Awake()
		{
			SubscribeGridObjectEvents();
		}

		private void Start()
		{
			UpdateText();
		}

		private void OnDestroy()
		{
			UnsubscribeGridObjectEvents();
		}

		private void SubscribeGridObjectEvents()
		{
			if (_gridObject != null)
				_gridObject.OnUnitsChanged += GridObject_OnUnitChanged;
		}

		private void UnsubscribeGridObjectEvents()
		{
			if (_gridObject != null)
				_gridObject.OnUnitsChanged -= GridObject_OnUnitChanged;
		}

		private void GridObject_OnUnitChanged(GridObject _)
		{
			Debug.Log("Changed unit");
			UpdateText();
		}

		private void UpdateText()
		{
			_text.text = _gridObject.ToString();
		}
	}
}
