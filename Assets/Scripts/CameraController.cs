using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class CameraController : MonoBehaviour
	{
		float _angleYVelocity = 0f;

		private void Update()
		{
			Move();
			Rotate();
		}

		private void Move()
		{
			const float moveSpeed = 10f;

			Vector3 inputDirection = Vector3.zero;

			inputDirection.x = Input.GetAxis("Horizontal");
			inputDirection.z = Input.GetAxis("Vertical");

			Vector3 moveMotion = transform.forward * inputDirection.z + transform.right * inputDirection.x;
			transform.position += moveSpeed * Time.deltaTime * moveMotion;
		}

		private void Rotate()
		{
			const float rotationSpeed = 135f;
			const float rotationDamp = .5f;

			float angleY = 0;

			if (Input.GetKey(KeyCode.E))
				angleY += rotationSpeed;
			if (Input.GetKey(KeyCode.Q))
				angleY -= rotationSpeed;

			angleY = Mathf.SmoothDamp(_angleYVelocity, angleY, ref _angleYVelocity, rotationDamp);
			transform.rotation *= Quaternion.Euler(0, angleY * Time.deltaTime, 0);
		}

	}
}
