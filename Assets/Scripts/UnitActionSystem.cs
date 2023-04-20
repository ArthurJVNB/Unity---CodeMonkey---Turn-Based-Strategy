using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[RequireComponent(typeof(MouseWorld))]
	public class UnitActionSystem : MonoBehaviour
	{
		[SerializeField] private Unit _selectedUnit;

		private MouseWorld _mouse;

		private void Awake()
		{
			_mouse = GetComponent<MouseWorld>();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (TrySelectUnit()) return;
				
			}

			if (Input.GetMouseButtonDown(1))
				TryMoveSelectedUnit();
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
				if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
				{
					_selectedUnit = unit;
					result = true;
				}
			}

			return result;
		}

		private void TryMoveSelectedUnit()
		{
			if (_selectedUnit)
				_selectedUnit.Move(_mouse.WorldPosition);
		}
	}
}
