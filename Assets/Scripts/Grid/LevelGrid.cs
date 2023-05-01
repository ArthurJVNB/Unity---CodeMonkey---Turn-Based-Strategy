using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Grid
{
	public class LevelGrid : MonoBehaviour
	{
		#region Singleton
		private static LevelGrid _instance;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			_instance = null;
		}

		public static LevelGrid Instance
		{
			get
			{
				if (_instance == null)
					_instance = new GameObject(nameof(LevelGrid)).AddComponent<LevelGrid>();

				return _instance;
			}

			private set
			{
				if (_instance != null)
					Destroy(_instance);

				_instance = value;
			}
		}
		#endregion

		[SerializeField] private GridDebugObject _gridDebugObjectPrefab;

		private GridSystem _grid;

		private void Awake()
		{
			Instance = this;
			_grid = new(10, 10, 2);

			if (_gridDebugObjectPrefab)
				_grid.CreateDebugObjects(_gridDebugObjectPrefab);
		}

		private void OnDestroy()
		{
			if (_instance == this)
				_instance = null;
		}

		public static List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
		{
			//return Instance._grid.GetGridObject(gridPosition).Unit;
			return Instance._grid.GetGridObject(gridPosition).Units;
		}

		public static void AddUnitAtGridPosition(Unit unit, GridPosition gridPosition)
		{
			//Instance._grid.GetGridObject(gridPosition).Unit = unit;
			Instance._grid.GetGridObject(gridPosition).AddUnit(unit);
		}

		public static void RemoveUnitAtGridPosition(Unit unit, GridPosition gridPosition)
		{
			//Instance._grid.GetGridObject(gridPosition).Unit = null;
			Instance._grid.GetGridObject(gridPosition).RemoveUnit(unit);
		}

		public static GridPosition GetGridPosition(Vector3 worldPosition)
		{
			return Instance._grid.GetGridPosition(worldPosition);
		}

		public static void MoveUnitGridPosition(Unit unit, GridPosition from, GridPosition to)
		{
			RemoveUnitAtGridPosition(unit, from);
			AddUnitAtGridPosition(unit, to);
		}
	}
}
