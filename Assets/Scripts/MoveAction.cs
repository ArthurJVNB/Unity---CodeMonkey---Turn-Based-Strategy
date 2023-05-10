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
						GridPosition test = currentGridPosition + offset;
						Debug.Log(test);
					}
				}

				return _validGridPositions;
			}
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
				transform.position += moveSpeed * Time.deltaTime * moveDirection;
				UpdateAnimator();
				_unit.UpdateGridPosition();

				transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
			}
			else if (IsMoving)
			{
				IsMoving = false;
				transform.position = _targetPosition;
				UpdateAnimator();
				_unit.UpdateGridPosition();
			}
		}

		public void Move(Vector3 targetPosition)
		{
			_targetPosition = targetPosition;
		}

		private void UpdateAnimator()
		{
			const string b_isWalking = "IsWalking";

			if (_animator)
				_animator.SetBool(b_isWalking, IsMoving);
		}
	}
}
