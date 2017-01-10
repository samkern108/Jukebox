using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	private LineRenderer line;

	public List<Vector3> levelPath;

	public void InitializePath() {
		line = GetComponent <LineRenderer>();
	
		line.numPositions = levelPath.Count;

		Vector3 newPos;

		for (int i = 0; i < levelPath.Count; i++) {
			newPos = levelPath [i];
			newPos *= BeatMaster.beatSize;
			newPos.y += (int)(Mathf.Ceil (BeatMaster.beatsAcrossHeight / 2) * BeatMaster.beatSize);
			levelPath [i] = newPos;
			line.SetPosition (i, levelPath[i]);
		}
	}

	public Vector3 GetWaypoint(int index) {
		return index < levelPath.Count ? levelPath [index] : levelPath[levelPath.Count - 1];
	}
}
