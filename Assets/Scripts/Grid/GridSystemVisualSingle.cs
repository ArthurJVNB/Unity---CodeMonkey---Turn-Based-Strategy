using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Grid
{
	public class GridSystemVisualSingle : MonoBehaviour
	{
		[SerializeField] private MeshRenderer _meshRenderer;

		public void Show()
		{
			_meshRenderer.enabled = true;
		}

		public void Hide()
		{
			_meshRenderer.enabled = false;
		}
	}
}
