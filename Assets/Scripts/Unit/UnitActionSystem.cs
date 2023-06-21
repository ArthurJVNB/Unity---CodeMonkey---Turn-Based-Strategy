using SW.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SW
{
	[RequireComponent(typeof(MouseWorld))]
	public class UnitActionSystem : MonoBehaviour
	{
		#region Singleton
		private static UnitActionSystem _instance;

		public static UnitActionSystem Instance
		{
			get
			{
				if (!_instance)
					_instance = new GameObject(nameof(UnitActionSystem)).AddComponent<UnitActionSystem>();
				return _instance;
			}
		}
		#endregion

		public static event EventHandler OnChangedSelectedUnit;
		public static event EventHandler OnChangedSelectedAction;
		public static BaseAction[] CurrentActions { get; private set; }

		public static Unit SelectedUnit
		{
			get => Instance._selectedUnit;
			private set
			{
				Instance._selectedUnit = value;
				if (value != null)
				{
					CurrentActions = value.GetComponents<BaseAction>();
					if (_instance._tryGetMoveActionOnSelection)
						SelectedAction = value.TryGetMoveAction(out MoveAction moveAction) ? moveAction : null;
					else
						SelectedAction = null;
				}
				else
				{
					CurrentActions = null;
					SelectedAction = null;
				}

				OnChangedSelectedUnit?.Invoke(_instance, EventArgs.Empty);
			}
		}

		public static BaseAction SelectedAction
		{
			get => Instance._selectedAction;
			set
			{
				Instance._selectedAction = value;
				OnChangedSelectedAction?.Invoke(_instance, EventArgs.Empty);
			}
		}

		public static void DeselectCurrentAction() => SelectedAction = null;



		[SerializeField] private Unit _selectedUnit;
		[SerializeField] private bool _tryGetMoveActionOnSelection = true;

		private MouseWorld _mouse;
		private bool _isBusy = false;
		private BaseAction _selectedAction;

		private void OnValidate()
		{
			CheckInstancesInScene();
		}

		private void Awake()
		{
			_instance = this;
			CheckInstancesInScene();
			SelectedUnit = _selectedUnit;
			_mouse = GetComponent<MouseWorld>();
		}

		private void Update()
		{
			if (_isBusy) return;

			if (EventSystem.current.IsPointerOverGameObject()) return;

			if (Input.GetMouseButtonDown(0))
			{
				if (TrySelectUnit()) return;

				HandleSelectedAction();
			}
		}

		private void SetBusy() => _isBusy = true;

		private void ClearBusy() => _isBusy = false;

		private void CheckInstancesInScene()
		{
			UnitActionSystem[] instances = FindObjectsByType<UnitActionSystem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			if (instances.Length > 1)
				Debug.LogWarning($"There's more than one UnitActionSystem!");
		}

		private void HandleSelectedAction()
		{
			//switch (SelectedAction)
			//{
			//	case MoveAction:
			//		TryMoveSelectedUnit();
			//		break;
			//	case SpinAction:
			//		TrySpinSelectedUnit();
			//		break;
			//}
			if (SelectedAction == null) return;

			GridPosition mouseGridPosition = LevelGrid.GetGridPosition(_mouse.WorldPosition);
			if (SelectedAction.TakeAction(mouseGridPosition, ClearBusy))
				SetBusy();
		}

		/// <summary>
		/// Tries to select an unit.
		/// </summary>
		/// <returns>Returns true if selected a unit.</returns>
		private bool TrySelectUnit()
		{
			bool result = false;

			if (_mouse.TryGetUnit(out var hitInfo))
			{
				if (hitInfo.transform.TryGetComponent(out Unit unit))
				{
					if (_selectedUnit != unit)
					{
						SelectedUnit = unit;
						result = true;
					}
				}
			}

			return result;
		}

		//private void TryMoveSelectedUnit()
		//{
		//	if (_selectedUnit)
		//		if (_selectedUnit.TryGetMoveAction(out MoveAction moveAction))
		//		{
		//			SetBusy();
		//			moveAction.TakeAction(LevelGrid.GetGridPosition(_mouse.WorldPosition), ClearBusy);
		//		}
		//}

		//private void TrySpinSelectedUnit()
		//{
		//	if (_selectedUnit)
		//		if (_selectedUnit.TryGetComponent(out SpinAction spinAction))
		//		{
		//			SetBusy();
		//			spinAction.TakeAction(ClearBusy);
		//		}
		//}
	}
}
