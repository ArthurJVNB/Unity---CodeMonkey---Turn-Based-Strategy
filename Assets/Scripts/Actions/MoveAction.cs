using SW.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[RequireComponent(typeof(Unit))]
	public class MoveAction : BaseAction
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private int _maxMoveDistance = 4;

		private Vector3 _targetPosition;
		private List<GridPosition> _validGridPositions;

		public bool IsMoving { get; private set; }

		public List<GridPosition> ValidGridPositions
		{
			get
			{
				_validGridPositions ??= new();
				_validGridPositions.Clear();

				GridPosition currentGridPosition = _unit.CurrentGridPosition;

				for (int x = -_maxMoveDistance; x < _maxMoveDistance + 1; x++)
				{
					for (int z = -_maxMoveDistance; z < _maxMoveDistance + 1; z++)
					{
						GridPosition offset = new(x, z);
						GridPosition target = currentGridPosition + offset;

						if (!LevelGrid.IsValidGridPosition(target))
							continue;

						if (target == _unit.CurrentGridPosition)
							continue;

						if (LevelGrid.HasAnyUnitAtGridPosition(target))
							continue;

						_validGridPositions.Add(target);
					}
				}

				return _validGridPositions;
			}
		}

		public bool IsValidPosition(GridPosition gridPosition)
		{
			return ValidGridPositions.Contains(gridPosition);
		}

		protected override void Awake()
		{
			base.Awake();
			_targetPosition = transform.position;
		}

		private void Update()
		{
			if (!IsActive)
			{
				enabled = false;
				return;
			}

			const float stoppingDistance = .1f;
			const float moveSpeed = 4f;
			const float rotationSpeed = 10f;

			if (Vector3.Distance(_targetPosition, transform.position) > stoppingDistance)
			{
				IsMoving = true;
				Vector3 moveDirection = (_targetPosition - transform.position).normalized;

				//// Movement
				//transform.position += moveSpeed * Time.deltaTime * moveDirection;

				//// Old Rotation (bugged if doing a perfect 180º rotation)
				//transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);

				//// New Rotation
				//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), rotationSpeed * Time.deltaTime);

				// Optimized Movement and Rotation Lerping
				transform.SetLocalPositionAndRotation(transform.position + moveSpeed * Time.deltaTime * moveDirection,
										  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), rotationSpeed * Time.deltaTime));

				UpdateAnimator();
				_unit.UpdateGridPosition();

				Debug.DrawLine(transform.position, transform.position + transform.forward * moveSpeed, Color.blue, .1f);
				Debug.DrawLine(transform.position, transform.position + moveDirection * moveSpeed, Color.green, .1f);
			}
			else if (IsMoving)
			{
				IsMoving = false;
				transform.position = _targetPosition;
				UpdateAnimator();
				_unit.UpdateGridPosition();
			}
			else
				CompleteAction();

		}

		public bool Move(Vector3 targetPosition, Action onActionComplete)
		{
			return Move(LevelGrid.GetGridPosition(targetPosition), onActionComplete);
		}

		public bool Move(GridPosition targetGridPosition, Action onActionComplete)
		{
			StartAction(onActionComplete);

			if (IsValidPosition(targetGridPosition))
			{
				_targetPosition = LevelGrid.GetWorldPosition(targetGridPosition);
				enabled = true;
				return true;
			}

			CompleteAction();
			return false;
		}

		private void UpdateAnimator()
		{
			const string b_isWalking = "IsWalking";

			if (_animator)
				_animator.SetBool(b_isWalking, IsMoving);
		}

		protected override void CompleteAction()
		{
			base.CompleteAction();
			enabled = false;
		}
	}
}
