using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Spawner : MonoBehaviour {

	private float spawnRate = 1.0f;
	private int spawnTotal = 100;
	private int spawned = 0;

	private GameObject enemy, start, goal;

	public void Start() {
		enemy = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.Enemy);

		start = GameObject.Find ("Spawner");
		goal = GameObject.Find ("Goal");

		Timing.RunCoroutine (Co_Spawn());
	}

	public void OnGUI() {
		if (Event.current.type == EventType.Repaint) {
			Debug.Log ("ONGUI");
			Drawing.DrawLine (new Vector2 (1, 0), new Vector2 (2, 4), Color.red, 10, true);
			Drawing.DrawCircle (new Vector2 (1, 1), 3, Color.blue, 1, false, 1);
		}
	}

	private IEnumerator<float> Co_Spawn() {
		while(spawned < spawnTotal) {
			Instantiate (enemy).GetComponent<Enemy>().Initialize(start.transform, goal.transform);
			spawned ++;
			yield return Timing.WaitForSeconds(spawnRate);
		}
	}
}
