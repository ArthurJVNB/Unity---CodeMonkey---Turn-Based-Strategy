using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	private Vector3 _targetPosition;

	private void Update()
	{
		const float stoppingDistance = .01f;
		const float moveSpeed = 4f;
		if (Vector3.Distance(_targetPosition, transform.position) > stoppingDistance)
		{
			Vector3 moveDirection = (_targetPosition - transform.position).normalized;
			transform.position += moveSpeed * Time.deltaTime * moveDirection;
		}
		else
			transform.position = _targetPosition;

		if (Input.GetKeyDown(KeyCode.Space))
			Move(new Vector3(4, 0, 4));
	}

	private void Move(Vector3 targetPosition)
	{
		_targetPosition = targetPosition;
	}
}
