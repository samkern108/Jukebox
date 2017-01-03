using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	private LineRenderer line;

	public Transform[] points;

	void Start () {
		line = GetComponent <LineRenderer>();

		points = transform.GetComponentsInChildren<Transform> ();

		line.SetVertexCount (points.Length);

		for (int i = 0; i < points.Length; i++) {
			line.SetPosition (i, points[i].position);
		}
	}
}
