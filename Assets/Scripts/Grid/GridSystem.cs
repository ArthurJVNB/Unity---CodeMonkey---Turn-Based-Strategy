using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Grid
{
	public class GridSystem
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public float CellSize { get; private set; }

		private readonly GridObject[,] _gridObjects;

		public GridSystem(int width, int height, float cellSize)
		{
			Width = width;
			Height = height;
			CellSize = cellSize;
			_gridObjects = new GridObject[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int z = 0; z < height; z++)
				{
					_gridObjects[x, z] = new GridObject(this, new GridPosition(x, z));
				}
			}
		}

		public Vector3 GetWorldPosition(GridPosition gridPosition)
		{
			return GetWorldPosition(gridPosition.x, gridPosition.z);
		}

		public Vector3 GetWorldPosition(int x, int z)
		{
			return new Vector3(x, 0, z) * CellSize;
		}

		public GridPosition GetGridPosition(Vector3 worldPosition)
		{
			return new GridPosition(
				Mathf.RoundToInt(worldPosition.x / CellSize),
				Mathf.RoundToInt(worldPosition.z / CellSize));
		}

		public GridObject GetGridObject(GridPosition gridPosition)
		{
			return _gridObjects[gridPosition.x, gridPosition.z];
		}

		public bool IsValidGridPosition(GridPosition gridPosition)
		{
			return gridPosition.x >= 0 && gridPosition.z >= 0 && gridPosition.x < Width && gridPosition.z < Height;
		}

		public void CreateDebugObjects(GridDebugObject debugPrefab)
		{
			Transform parent = new GameObject("--- Grid Debug Objects ---").transform;
			for (int x = 0; x < Width; x++)
			{
				for (int z = 0; z < Height; z++)
				{
					var debugInstance = Object.Instantiate(debugPrefab, GetWorldPosition(x,z), Quaternion.identity, parent);
					debugInstance.GridObject = GetGridObject(new GridPosition(x,z));
				}
			}
		}

	}
}
