using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[System.Obsolete("Use MouseWorld instead.")]
	public class MouseWorldStatic : MonoBehaviour
	{
		[SerializeField] private LayerMask _mouseWorldCollisionLayer = 1 << 6;

		private static LayerMask _updatedLayerMask = 1 << 0;

		public static bool GetWorldPosition(out Vector3 worldPosition)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _updatedLayerMask);
			worldPosition = hitInfo.point;
			return hasHit;
		}

		private void OnValidate()
		{
			var mouseWorld = FindObjectsOfType<MouseWorldStatic>();
			if (mouseWorld == null || mouseWorld.Length <= 1)
				return;

			Debug.LogWarning($"There is more than one {nameof(MouseWorldStatic)} inside the scene. You must garantee that there is always only one.\n" +
				$"Unexpected behaviour might occur when this happens.\n" +
				$"The following logs will list every {nameof(MouseWorldStatic)} found in the scene.");

			for (int i = 0; i < mouseWorld.Length; i++)
				Debug.LogWarning($"{mouseWorld[i].name}", mouseWorld[i]);
		}

		private void Awake()
		{
			_updatedLayerMask = _mouseWorldCollisionLayer;
		}

		private void Update()
		{
			if (GetWorldPosition(out Vector3 worldPosition))
				transform.position = worldPosition;
		}
	}
}
