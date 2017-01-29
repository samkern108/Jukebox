using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EX_LineRenderer {
	
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
