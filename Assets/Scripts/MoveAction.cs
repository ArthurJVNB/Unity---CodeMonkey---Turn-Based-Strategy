using SW.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[RequireComponent(typeof(Unit))]
	public class MoveAction : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private int _maxMoveDistance = 4;

		private Unit _unit;
		private Vector3 _targetPosition;
		private List<GridPosition> _validGridPositions;

		public bool IsMoving { get; private set; }

		public List<GridPosition> ValidGridPositions
		{
			get
			{
				_validGridPositions ??= new();
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

		private void Awake()
		{
			_unit = GetComponent<Unit>();
			_targetPosition = transform.position;
		}

		private void Update()
		{
			const float stoppingDistance = .1f;
			const float moveSpeed = 4f;
			const float rotationSpeed = 10f;

			if (Vector3.Distance(_targetPosition, transform.position) > stoppingDistance)
			{
				IsMoving = true;
				Vector3 moveDirection = (_targetPosition - transform.position).normalized;

				//// Movement
				//transform.position += moveSpeed * Time.deltaTime * moveDirection;

				//// Old Rotation (bugged if doing a perfect 180� rotation)
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
		}

		public bool Move(Vector3 targetPosition)
		{
			return Move(LevelGrid.GetGridPosition(targetPosition));
		}

		public bool Move(GridPosition targetGridPosition)
		{
			if (IsValidPosition(targetGridPosition))
			{
				_targetPosition = LevelGrid.GetWorldPosition(targetGridPosition);
				return true;
			}

			return false;
		}

		private void UpdateAnimator()
		{
			const string b_isWalking = "IsWalking";

			if (_animator)
				_animator.SetBool(b_isWalking, IsMoving);
		}
	}
}
