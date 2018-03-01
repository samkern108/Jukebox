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
	
		line.positionCount = points.Length;

		Vector3 newPos;
		for (int i = 0; i < points.Length; i++) {
			newPos = BeatMaster.GetClosestGridCellBotLeft (points [i] [0], points [i] [1]);
			levelPath.Add (newPos);
			line.SetPosition (i, levelPath[i]);
		}

		GameObject p_Spawner = ResourceLoader.LoadPrefab (ResourceNamePrefab.Spawner);
		Instantiate (p_Spawner).GetComponent<Spawner>().InitializeSpawner(this, pathJSON.spawner);

		endColor = Palette.colorNames[pathJSON.endColor];
		GameObject endSprite = transform.Find ("End").gameObject;
		endSprite.GetComponent <SpriteRenderer>().color = endColor;
		endSprite.transform.position = levelPath[levelPath.Count - 1];
	}

	public Vector3 GetWaypoint(int index) {
		return index < levelPath.Count ? levelPath [index] : levelPath[levelPath.Count - 1];
	}
}
