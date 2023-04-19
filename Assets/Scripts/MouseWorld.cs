using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class MouseWorld : MonoBehaviour
	{
		[SerializeField] private LayerMask _mouseWorldCollisionLayer = 1 << 6;

		public Vector3 WorldPosition
		{
			get
			{
				TryGetWorldPosition(out Vector3 worldPosition);
				return worldPosition;
			}
		}

		public bool TryGetWorldPosition(out Vector3 worldPosition)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			bool result = Physics.Raycast(ray, out var hitInfo, float.MaxValue, _mouseWorldCollisionLayer);
			worldPosition = hitInfo.point;
			return result;
		}
	}
}
