using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	[RequireComponent(typeof(MeshRenderer))]
	public class UnitSelectedVisual : MonoBehaviour
	{
		[SerializeField] private Unit _unit;
		
		private MeshRenderer _meshRenderer;

		private void Awake()
		{
			_meshRenderer = GetComponent<MeshRenderer>();
		}

		private void Start()
		{
			UnitActionSystem.OnChangedSelectedUnit += UnitActionSystem_OnChangedSelectedUnit;
			UpdateVisual();
		}

		private void OnDestroy()
		{
			UnitActionSystem.OnChangedSelectedUnit -= UnitActionSystem_OnChangedSelectedUnit;
		}

		private void UnitActionSystem_OnChangedSelectedUnit(object sender, System.EventArgs e)
		{
			UpdateVisual();
		}

		private void UpdateVisual()
		{
			_meshRenderer.enabled = UnitActionSystem.SelectedUnit == _unit;
		}
	}
}
