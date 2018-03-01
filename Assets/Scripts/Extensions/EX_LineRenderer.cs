using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EX_LineRenderer {
	
	public static void DrawCircle(this LineRenderer line, float radius, int numPositions) {
		float x, y;
		float delta = (2 * Mathf.PI) / numPositions;
		line.positionCount = numPositions + 1;
		for (int i = 0; i < (numPositions + 1); i++) 
		{
			x = radius * Mathf.Cos (delta * i);
			y = radius * Mathf.Sin (delta * i);
			line.SetPosition (i, line.transform.position + new Vector3(x, y, 0));
		}
	}

	public static void DrawBox(this LineRenderer line, float side) {
		side = side / 2;
		line.positionCount = 5;
		// TODO(samkern): If I don't use world space or something, will I still have to add line.transform.position?
		line.SetPosition (0, line.transform.position + new Vector3(side, side, 0));
		line.SetPosition (1, line.transform.position + new Vector3(side, -side, 0));
		line.SetPosition (2, line.transform.position + new Vector3(-side, -side, 0));
		line.SetPosition (3, line.transform.position + new Vector3(-side, side, 0));
		line.SetPosition (4, line.transform.position + new Vector3(side, side, 0));
	}

	// Right now this just draws a triangle because I'm lazy as fuck
	public static void DrawCone(this LineRenderer line, Vector2[] points) {

		//ugh unity why
		Vector3[] pointsUgh = new Vector3[points.Length];
		for (int i = 0; i < points.Length; i++)
			pointsUgh [i] = points [i];
		
		line.SetPositions (pointsUgh);
	}
}
