using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	private LineRenderer line;

	public List<Transform> points;

	void Awake () {
		line = GetComponent <LineRenderer>();
		points = new List<Transform>(transform.GetComponentsInChildren<Transform> ());
		points.RemoveAt (0);

		line.numPositions = points.Count;

		for (int i = 0; i < points.Count; i++) {
			line.SetPosition (i, points[i].position);
		}
	}

	public Transform GetWaypoint(int index) {
		return index < points.Count ? points [index] : null;
	}
}
