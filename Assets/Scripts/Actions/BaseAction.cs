using SW.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public abstract class BaseAction : MonoBehaviour
	{
		[field: SerializeField] public bool IsActive { get; private set; }

		protected Unit _unit;
		private Action _onActionComplete;

		public abstract string Name { get; }
		public virtual int ActionCost => 1;

		protected virtual void Awake() => _unit = GetComponent<Unit>();

		public virtual bool CanTakeAction(GridPosition gridPosition)
		{
			return IsValidPosition(gridPosition);
		}

		public abstract bool TakeAction(GridPosition gridPosition, Action onActionComplete);

		protected virtual void StartAction(Action onActionCompleted)
		{
			IsActive = true;
			_onActionComplete = onActionCompleted;
		}

		protected virtual void CompleteAction()
		{
			IsActive = false;
			_onActionComplete();
		}

		public abstract List<GridPosition> ValidGridPositions { get; }

		public virtual bool IsValidPosition(GridPosition gridPosition)
		{
			return ValidGridPositions.Contains(gridPosition);
		}
	}
}
