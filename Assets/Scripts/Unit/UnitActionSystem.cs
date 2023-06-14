using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[RequireComponent(typeof(MouseWorld))]
	public class UnitActionSystem : MonoBehaviour
	{
		public static event EventHandler OnChangedSelectedUnit;
		public static Unit CurrentSelectedUnit { get; private set; }

		[SerializeField] private Unit _selectedUnit;

		private MouseWorld _mouse;

		public Unit SelectedUnit
		{
			get => _selectedUnit;
			private set
			{
				_selectedUnit = value;
				CurrentSelectedUnit = value;
				OnChangedSelectedUnit?.Invoke(this, EventArgs.Empty);
			}
		}

		private void OnValidate()
		{
			CheckInstancesInScene();
		}

		private void Awake()
		{
			CheckInstancesInScene();
			SelectedUnit = _selectedUnit;
			_mouse = GetComponent<MouseWorld>();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (TrySelectUnit()) return;
				TryMoveSelectedUnit();
			}

			if (Input.GetMouseButtonDown(1))
			{
				TrySpinSelectedUnit();
			}
		}

		private void CheckInstancesInScene()
		{
			UnitActionSystem[] instances = FindObjectsByType<UnitActionSystem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			if (instances.Length > 1)
				Debug.LogWarning($"There's more than one UnitActionSystem!");
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

		private void TryMoveSelectedUnit()
		{
			if (_selectedUnit)
				if (_selectedUnit.TryGetMoveAction(out MoveAction moveAction))
					moveAction.Move(_mouse.WorldPosition);
		}

		private void TrySpinSelectedUnit()
		{
			if (_selectedUnit)
				if (_selectedUnit.TryGetComponent(out SpinAction spinAction))
					spinAction.Spin();
		}
	}
}
