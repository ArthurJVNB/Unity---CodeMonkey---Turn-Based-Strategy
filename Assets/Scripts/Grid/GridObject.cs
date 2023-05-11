using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Grid
{
	public class GridObject
	{
		public event Action<GridObject> OnUnitsChanged;

		private GridSystem _gridSystem;
		private GridPosition _gridPosition;
		private List<Unit> _units;

		public List<Unit> Units => _units;

		public GridObject(GridSystem gridSystem, GridPosition gridPosition)
		{
			_gridSystem = gridSystem;
			_gridPosition = gridPosition;
			_units = new();
		}

		public GridObject(GridSystem gridSystem, int x, int z)
		{
			new GridObject(gridSystem, new GridPosition(x, z));
		}

		public bool HasAnyUnit => _units.Count > 0;

		public void AddUnit(Unit unit)
		{
			_units.Add(unit);
			OnUnitsChanged?.Invoke(this);
		}

		public bool RemoveUnit(Unit unit)
		{
			bool result = _units.Remove(unit);
			OnUnitsChanged?.Invoke(this);
			return result;
		}

		public void ClearUnits()
		{
			_units.Clear();
			OnUnitsChanged?.Invoke(this);
		}

		public override string ToString()
		{
			System.Text.StringBuilder sb = new();
			sb.AppendLine($"{_gridPosition.x}, {_gridPosition.z}");
			if (_units != null && _units.Count > 0)
			{
				for (int i = 0; i < _units.Count; i++)
					sb.AppendLine(_units[i].ToString());
			}

			return sb.ToString();
		}

	}
}
