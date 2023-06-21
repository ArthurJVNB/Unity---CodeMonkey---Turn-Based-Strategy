using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SW
{
	public class ActionButtonUI : MonoBehaviour, ISelectable
	{
		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _text;
		[SerializeField] private GameObject _selection;

		public BaseAction Action { get; private set; }

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
			Action = action;
			_text.text = action.Name;
		}

		private void OnClick()
		{
			if (UnitActionSystem.SelectedAction == Action)
				UnitActionSystem.DeselectCurrentAction();
			else
				UnitActionSystem.SelectedAction = Action;
		}

		public void Select()
		{
			_selection.SetActive(true);
		}

		public void Deselect()
		{
			_selection.SetActive(false);
		}
	}
}
