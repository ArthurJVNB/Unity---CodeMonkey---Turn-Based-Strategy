using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class MouseWorld : MonoBehaviour
	{
		[ContextMenuItem("Reset This Setting", nameof(ResetMouseWorldCollisionLayerSetting))]
		[SerializeField] private LayerMask _mouseWorldCollisionLayer = Layer.MouseCollision;
		[ContextMenuItem("Reset This Setting", nameof(ResetUnitLayerSetting))]
		[SerializeField] private LayerMask _unitLayer = Layer.Unit;

		#region EDITOR ONLY
		private void ResetMouseWorldCollisionLayerSetting()
		{
			_mouseWorldCollisionLayer = Layer.MouseCollision;
		}

		private void ResetUnitLayerSetting()
		{
			_unitLayer = Layer.Unit;
		}
		#endregion

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
			bool result = HitInfo(_mouseWorldCollisionLayer, out RaycastHit hitInfo);
			worldPosition = hitInfo.point;
			return result;
		}

		public bool TryGetUnit(out RaycastHit hitInfo)
		{
			return HitInfo(_unitLayer, out hitInfo);
		}

		public bool HitInfo(LayerMask layerMask, out RaycastHit hitInfo)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			return Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask);
		}
	}
}
