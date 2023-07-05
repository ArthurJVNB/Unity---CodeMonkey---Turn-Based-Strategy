using SW.Args;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public class ActionBusyUI : MonoBehaviour
	{
		private void Start()
		{
			UpdateUI(UnitActionSystem.IsBusy);
			UnitActionSystem.OnChangedBusy += UnitActionSystem_OnChangedBusy;
		}

		private void OnDestroy()
		{
			UnitActionSystem.OnChangedBusy -= UnitActionSystem_OnChangedBusy;
		}

		private void UnitActionSystem_OnChangedBusy(object sender, BusyArgs e)
		{
			UpdateUI(e.IsBusy);
		}

		private void UpdateUI(bool isBusy)
		{
			if (isBusy) Show();
			else Hide();
		}

		private void Show()
		{
			gameObject.SetActive(true);
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
