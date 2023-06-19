using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SW
{
	public class ActionButtonUI : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _text;

		private BaseAction _action;

		private void Awake()
		{
			_button.onClick.AddListener(OnClick);
		}

		private void OnDestroy()
		{
			_button.onClick.RemoveListener(OnClick);
		}

		public void SetBaseAction(BaseAction action)
		{
			_action = action;
			_text.text = action.Name;
		}

		private void OnClick()
		{
			UnitActionSystem.SelectedAction = _action;
		}
	}
}
