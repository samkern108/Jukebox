using UnityEngine;
using System.Collections;

public static class Extensions {

	public static float Map (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	public static void DrawCircle(this LineRenderer line, float radius, int numPositions) {
		float x, y;
		float delta = (2 * Mathf.PI) / numPositions;
		line.numPositions = numPositions + 1;
		for (int i = 0; i < (numPositions + 1); i++) 
		{
			x = radius * Mathf.Cos (delta * i);
			y = radius * Mathf.Sin (delta * i);
			line.SetPosition (i, line.transform.position + new Vector3(x, y, 0));
		}
	}

}

