using SW.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class SpinAction : BaseAction
	{
		private Quaternion _targetRotation;
		private float _spinAmount;
		private float _totalSpinAmount = float.MaxValue;
		private List<GridPosition> _validGridPositions;

		public override string Name => "Spin";

		public override int ActionCost => 2;

		public override List<GridPosition> ValidGridPositions
		{
			get
			{
				_validGridPositions ??= new();
				_validGridPositions.Clear();
				_validGridPositions.Add(UnitActionSystem.SelectedUnit.CurrentGridPosition);
				return _validGridPositions;
			}
		}

		private void Update()
		{
			if (!IsActive)
			{
				enabled = false;
				return;
			}

			_spinAmount = 360 * Time.deltaTime;
			_totalSpinAmount += _spinAmount;

			if (_totalSpinAmount > 360)
			{
				CompleteAction();
				return;
			}

			transform.eulerAngles += new Vector3(0, _spinAmount, 0);
		}

		public override bool CanTakeAction(GridPosition _)
		{
			return true;
		}

		public override bool TakeAction(GridPosition _, Action onActionComplete)
		{
			StartAction(onActionComplete);
			return true;
		}

		protected override void StartAction(Action onActionCompleted)
		{
			base.StartAction(onActionCompleted);
			_totalSpinAmount = 0;
			_targetRotation = transform.rotation;
			enabled = true;
		}

		protected override void CompleteAction()
		{
			base.CompleteAction();
			transform.rotation = _targetRotation;
			enabled = false;
		}
	}
}
