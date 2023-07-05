using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Args
{
	public class SpentActionPointArgs : EventArgs
	{
		public Unit Unit;
		public int ActionPointsSpent;
		public int RemainingActionPoints;

		public SpentActionPointArgs(Unit unit, int actionPointsSpent, int remainingActionPoints)
		{
			Unit = unit;
			ActionPointsSpent = actionPointsSpent;
			RemainingActionPoints = remainingActionPoints;
		}
	}
}
