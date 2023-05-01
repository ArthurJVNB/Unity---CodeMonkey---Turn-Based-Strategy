using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Grid
{
	public class GridSystem
	{
		public int width;
		public int height;
		public float cellSize;

		private GridObject[,] gridObjects;

		public GridSystem(int width, int height, float cellSize)
		{
			this.width = width;
			this.height = height;
			this.cellSize = cellSize;
			gridObjects = new GridObject[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int z = 0; z < height; z++)
				{
					gridObjects[x, z] = new GridObject(this, new GridPosition(x, z));
				}
			}
		}

		public Vector3 GetWorldPosition(GridPosition gridPosition)
		{
			return GetWorldPosition(gridPosition.x, gridPosition.z);
		}

		public Vector3 GetWorldPosition(int x, int z)
		{
			return new Vector3(x, 0, z) * cellSize;
		}

		public GridPosition GetGridPosition(Vector3 worldPosition)
		{
			return new GridPosition(
				Mathf.RoundToInt(worldPosition.x / cellSize),
				Mathf.RoundToInt(worldPosition.z / cellSize));
		}

		public GridObject GetGridObject(GridPosition gridPosition)
		{
			return gridObjects[gridPosition.x, gridPosition.z];
		}

		public void CreateDebugObjects(GridDebugObject debugPrefab)
		{
			Transform parent = new GameObject("--- Grid Debug Objects ---").transform;
			for (int x = 0; x < width; x++)
			{
				for (int z = 0; z < height; z++)
				{
					var debugInstance = GameObject.Instantiate(debugPrefab, GetWorldPosition(x,z), Quaternion.identity, parent);
					debugInstance.GridObject = GetGridObject(new GridPosition(x,z));
				}
			}
		}
	}
}
