using SW.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class Unit : MonoBehaviour
	{
		[SerializeField] private Animator _animator;

		public bool IsMoving { get; private set; }

		private Vector3 _targetPosition;
		private GridPosition _lastGridPosition;

		private void Awake()
		{
			_targetPosition = transform.position;
		}

		private void Start()
		{
			_lastGridPosition = LevelGrid.GetGridPosition(transform.position);
			LevelGrid.AddUnitAtGridPosition(this, _lastGridPosition);
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
				UpdateGridPosition();

				transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
			}
			else if (IsMoving)
			{
				IsMoving = false;
				transform.position = _targetPosition;
				UpdateAnimator();
				UpdateGridPosition();
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

		private void UpdateGridPosition()
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
