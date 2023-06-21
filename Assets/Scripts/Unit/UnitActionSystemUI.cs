using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SW
{
	public class UnitActionSystemUI : MonoBehaviour
	{
		[SerializeField] private ActionButtonUI _actionButtonPrefab;
		[SerializeField] private Transform _actionButtonContainerTransform;

		private List<ActionButtonUI> _actionButtons;

		private void Awake()
		{
			_actionButtons = new();
		}

		private void Start()
		{
			UnitActionSystem.OnChangedSelectedUnit += UnitActionSystem_OnChangedSelectedUnit;
			UnitActionSystem.OnChangedSelectedAction += UnitActionSystem_OnChangedSelectedAction;

			if (UnitActionSystem.CurrentActions != null)
				CreateUnitActions();
			else ClearActions();
		}

		private void OnDestroy()
		{
			UnitActionSystem.OnChangedSelectedUnit -= UnitActionSystem_OnChangedSelectedUnit;
			UnitActionSystem.OnChangedSelectedAction -= UnitActionSystem_OnChangedSelectedAction;
		}

		private void UnitActionSystem_OnChangedSelectedUnit(object sender, System.EventArgs e)
		{
			CreateUnitActions();
			UpdateSelectedVisual();
		}

		private void UnitActionSystem_OnChangedSelectedAction(object sender, System.EventArgs e)
		{
			UpdateSelectedVisual();
		}

		private void CreateUnitActions()
		{
			ClearActions();
			foreach (BaseAction action in UnitActionSystem.CurrentActions)
			{
				ActionButtonUI button = Instantiate(_actionButtonPrefab, _actionButtonContainerTransform);
				button.SetBaseAction(action);
				_actionButtons.Add(button);
			}
			UpdateSelectedVisual();
		}

		private void ClearActions()
		{
			foreach (Transform actionTransform in _actionButtonContainerTransform)
			{
				Destroy(actionTransform.gameObject);
			}
			_actionButtons.Clear();
		}

		private void UpdateSelectedVisual()
		{
			BaseAction selectedAction = UnitActionSystem.SelectedAction;
			foreach (ActionButtonUI action in _actionButtons)
			{
				if (action.Action == selectedAction)
					action.Select();
				else
					action.Deselect();
			}
		}
	}
}
