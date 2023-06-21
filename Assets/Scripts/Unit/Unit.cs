using SW.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class Unit : MonoBehaviour
	{
		private GridPosition _lastGridPosition;
		private MoveAction _moveAction;

		public MoveAction MoveAction
		{
			get
			{
				if (_moveAction == null)
					_moveAction = GetComponent<MoveAction>();
				return _moveAction;
			}
		}

		public GridPosition CurrentGridPosition => _lastGridPosition;

		private void Start()
		{
			_lastGridPosition = LevelGrid.GetGridPosition(transform.position);
			LevelGrid.AddUnitAtGridPosition(this, _lastGridPosition);
		}

		public bool TryGetMoveAction(out MoveAction moveAction)
		{
			moveAction = MoveAction;
			return moveAction != null;
		}

		public void UpdateGridPosition()
		{
			GridPosition currentGridPosition = LevelGrid.GetGridPosition(transform.position);
			if (_lastGridPosition != currentGridPosition)
			{
				LevelGrid.MoveUnitGridPosition(this, _lastGridPosition, currentGridPosition);
				_lastGridPosition = currentGridPosition;
			}
		}

		public override string ToString()
		{
			return name;
		}

	}
}
