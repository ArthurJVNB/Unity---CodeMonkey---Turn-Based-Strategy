using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public abstract class BaseAction : MonoBehaviour
	{
		[field: SerializeField]
		public bool IsActive { get; private set; }
		protected Unit _unit;
		private Action _onActionComplete;

		protected virtual void Awake() => _unit = GetComponent<Unit>();

		public abstract string Name { get; }

		protected virtual void StartAction(Action onActionCompleted)
		{
			IsActive = true;
			_onActionComplete = onActionCompleted;
		}

		protected virtual void CompleteAction()
		{
			IsActive = false;
			_onActionComplete();
		}

	}
}
