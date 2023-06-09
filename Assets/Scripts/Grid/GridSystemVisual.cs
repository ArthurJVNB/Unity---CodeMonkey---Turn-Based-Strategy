using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Grid
{
	public class GridSystemVisual : MonoBehaviour
	{
		[SerializeField] private GridSystemVisualSingle _gridSystemVisualSinglePrefab;

		private GridSystemVisualSingle[,] _gridVisualPositions;

		private void Start()
		{
			int cellSize = LevelGrid.GridCellSize;
			int width = LevelGrid.GridWidth;
			int height = LevelGrid.GridHeight;
			_gridVisualPositions = new GridSystemVisualSingle[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int z = 0; z < height; z++)
				{
					_gridVisualPositions[x, z] = Instantiate(_gridSystemVisualSinglePrefab, new Vector3(x * cellSize, 0, z * cellSize), Quaternion.identity, transform);
				}
			}
		}

		private void Update()
		{
			UpdateGridVisual();
		}

		public void HideAllPositions()
		{
			foreach (var position in _gridVisualPositions)
			{
				position.Hide();
			}
		}

		public void ShowPositions(List<GridPosition> gridPositions)
		{
			foreach (var gridPosition in gridPositions)
			{
				_gridVisualPositions[gridPosition.x, gridPosition.z].Show();
			}
		}

		private void UpdateGridVisual()
		{
			if (UnitActionSystem.CurrentSelectedUnit.TryGetMoveAction(out MoveAction moveAction))
			{
				HideAllPositions();
				ShowPositions(moveAction.ValidGridPositions);
			}
		}
	}
}
