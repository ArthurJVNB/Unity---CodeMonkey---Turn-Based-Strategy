using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW
{
	public abstract class BaseAction : MonoBehaviour
	{
		protected Unit _unit;

		protected virtual void Awake() => _unit = GetComponent<Unit>();

	}
}
