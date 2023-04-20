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

		private void Awake()
		{
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

				transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
			}
			else if (IsMoving)
			{
				IsMoving = false;
				transform.position = _targetPosition;
				UpdateAnimator();
			}
		}

		public void Move(Vector3 targetPosition)
		{
			_targetPosition = targetPosition;
		}

		private void UpdateAnimator()
		{
			const string isWalking = "IsWalking";

			if (_animator)
				_animator.SetBool(isWalking, IsMoving);
		}
	}
}
