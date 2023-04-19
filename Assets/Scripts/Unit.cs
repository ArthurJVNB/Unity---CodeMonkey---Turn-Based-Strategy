using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[RequireComponent(typeof(MouseWorld))]
	public class Unit : MonoBehaviour
	{
		private MouseWorld _mouse;
		private Vector3 _targetPosition;

		private void Awake()
		{
			_mouse = GetComponent<MouseWorld>();
		}

		private void Update()
		{
			const float stoppingDistance = .1f;
			const float moveSpeed = 4f;
			if (Vector3.Distance(_targetPosition, transform.position) > stoppingDistance)
			{
				Vector3 moveDirection = (_targetPosition - transform.position).normalized;
				transform.position += moveSpeed * Time.deltaTime * moveDirection;
			}
			else
				transform.position = _targetPosition;

			if (Input.GetMouseButton(0))
				Move(_mouse.WorldPosition);
		}

		private void Move(Vector3 targetPosition)
		{
			_targetPosition = targetPosition;
		}
	}
}
