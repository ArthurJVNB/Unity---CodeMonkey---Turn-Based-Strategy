using SW.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class Testing : MonoBehaviour
	{
		//[SerializeField] private UnitActionSystem _unitActionSystem;
		[SerializeField] private GridSystemVisual _gridSystemVisual;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				UnitActionSystem.SelectedUnit.TryGetMoveAction(out var moveAction);
				_gridSystemVisual.HideAllPositions();
				_gridSystemVisual.ShowPositions(moveAction.ValidGridPositions);
			}
		}
	}
}
