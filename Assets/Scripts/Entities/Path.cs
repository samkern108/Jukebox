using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	private LineRenderer line;
	private List<Vector3> levelPath = new List<Vector3>();
	public Color endColor;

	public void InitializePath(PathJSON pathJSON) {
		line = GetComponent <LineRenderer>();

		int[][] points = pathJSON.points;
	
		line.numPositions = points.Length;

		Vector3 newPos;
		for (int i = 0; i < points.Length; i++) {
			newPos = new Vector3(points [i][0], points[i][1]);
			newPos *= BeatMaster.beatSize;
			newPos.y += (int)(Mathf.Ceil (BeatMaster.beatsAcrossHeight / 2) * BeatMaster.beatSize);
			levelPath.Add(newPos);
			line.SetPosition (i, levelPath[i]);
		}

		GameObject p_Spawner = ResourceLoader.LoadPrefab (ResourceNamePrefab.Spawner);
		Instantiate (p_Spawner).GetComponent<Spawner>().InitializeSpawner(this, pathJSON.spawner);

		endColor = new Color (pathJSON.endColor[0], pathJSON.endColor[1], pathJSON.endColor[2], 1);
		GameObject endSprite = transform.Find ("End").gameObject;
		endSprite.GetComponent <SpriteRenderer>().color = endColor;
		endSprite.transform.position = levelPath[levelPath.Count - 1];
	}

	public Vector3 GetWaypoint(int index) {
		return index < levelPath.Count ? levelPath [index] : levelPath[levelPath.Count - 1];
	}
}
