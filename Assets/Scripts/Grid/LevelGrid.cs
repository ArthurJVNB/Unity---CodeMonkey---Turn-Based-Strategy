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
		private static void Init() => _instance = null;

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

		[SerializeField] private int _width = 10;
		[SerializeField] private int _height = 10;
		[Tooltip("Cell size in meters")]
		[SerializeField] private int _cellSize = 2;
		[SerializeField] private GridDebugObject _gridDebugObjectPrefab;

		private GridSystem _gridSystem;

		private void Awake()
		{
			Instance = this;
			_gridSystem = new(_width, _height, _cellSize);

			if (_gridDebugObjectPrefab)
				_gridSystem.CreateDebugObjects(_gridDebugObjectPrefab);
		}

		private void OnDestroy()
		{
			if (_instance == this)
				_instance = null;
		}

		public static bool IsValidGridPosition(GridPosition gridPosition)
		{
			return Instance._gridSystem.IsValidGridPosition(gridPosition);
		}

		public static bool HasAnyUnitAtGridPosition(GridPosition gridPosition)
		{
			if (!Instance._gridSystem.IsValidGridPosition(gridPosition))
				return false;
			return Instance._gridSystem.GetGridObject(gridPosition).HasAnyUnit;
		}

		public static List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
		{
			if (Instance._gridSystem.IsValidGridPosition(gridPosition))
				return Instance._gridSystem.GetGridObject(gridPosition).Units;
			return new();
		}

		public static void AddUnitAtGridPosition(Unit unit, GridPosition gridPosition)
		{
			Instance._gridSystem.GetGridObject(gridPosition).AddUnit(unit);
		}

		public static void RemoveUnitAtGridPosition(Unit unit, GridPosition gridPosition)
		{
			Instance._gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
		}

		public static GridPosition GetGridPosition(Vector3 worldPosition)
		{
			return Instance._gridSystem.GetGridPosition(worldPosition);
		}

		public static Vector3 GetWorldPosition(GridPosition gridPosition)
		{
			return Instance._gridSystem.GetWorldPosition(gridPosition);
		}

		public static void MoveUnitGridPosition(Unit unit, GridPosition from, GridPosition to)
		{
			RemoveUnitAtGridPosition(unit, from);
			AddUnitAtGridPosition(unit, to);
		}
	}
}
