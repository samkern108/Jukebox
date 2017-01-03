using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	private LineRenderer line;

	public Transform[] points;

	void Awake () {
		line = GetComponent <LineRenderer>();

		points = transform.GetComponentsInChildren<Transform> ();

		line.numPositions = points.Length;

		for (int i = 0; i < points.Length; i++) {
			line.SetPosition (i, points[i].position);
		}
	}

	public Transform GetWaypoint(int index) {
		return index < points.Length ? points [index] : points[points.Length-1];
	}
}
