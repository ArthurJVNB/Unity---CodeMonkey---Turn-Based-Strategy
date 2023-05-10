using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class Testing : MonoBehaviour
	{
		[SerializeField] private UnitActionSystem _unitActionSystem;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_unitActionSystem.SelectedUnit.TryGetMoveAction(out var moveAction);
				_ = moveAction.ValidGridPositions;
			}
		}
	}
}
