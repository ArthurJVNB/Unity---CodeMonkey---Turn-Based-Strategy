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

		private void Start()
		{
			UnitActionSystem.OnChangedSelectedUnit += UnitActionSystem_OnChangedSelectedUnit;

			if (UnitActionSystem.CurrentActions != null)
				ShowCurrentActions();
		}

		private void OnDestroy()
		{
			UnitActionSystem.OnChangedSelectedUnit -= UnitActionSystem_OnChangedSelectedUnit;
		}

		private void UnitActionSystem_OnChangedSelectedUnit(object sender, System.EventArgs e)
		{
			ShowCurrentActions();
		}

		private void ShowCurrentActions()
		{
			ClearActions();
			foreach (BaseAction action in UnitActionSystem.CurrentActions)
			{
				ActionButtonUI button = Instantiate(_actionButtonPrefab, _actionButtonContainerTransform);
				button.SetBaseAction(action);
			}
		}

		private void ClearActions()
		{
			foreach (Transform buttonTransform in _actionButtonContainerTransform)
			{
				Destroy(buttonTransform.gameObject);
			}
		}
	}
}
