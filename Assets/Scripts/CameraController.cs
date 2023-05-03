using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private CinemachineVirtualCamera _virtualCamera;
		[SerializeField] private bool _invertZoom;
		
		private float _angleYVelocity = 0f;
		private CinemachineTransposer _cinemachineTransposer;
		private Vector3 _followOffsetTarget;

		private void Start()
		{
			_cinemachineTransposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
			_followOffsetTarget = _cinemachineTransposer.m_FollowOffset;
		}

		private void Update()
		{
			Move();
			Rotate();
			Zoom();
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

		private void Zoom()
		{
			const float minZoom = 2;
			const float maxZoom = 12;
			const float zoomSpeed = 8;

			if (Input.mouseScrollDelta.y < 0)
				_followOffsetTarget.y += _invertZoom ? -1 : 1;
			else if (Input.mouseScrollDelta.y > 0)
				_followOffsetTarget.y -= _invertZoom ? -1 : 1;

			_followOffsetTarget.y = Mathf.Clamp(_followOffsetTarget.y, minZoom, maxZoom);
			_cinemachineTransposer.m_FollowOffset.y = Mathf.Lerp(_cinemachineTransposer.m_FollowOffset.y, _followOffsetTarget.y, zoomSpeed * Time.deltaTime);
		}

	}
}
