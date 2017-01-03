using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Spawner : MonoBehaviour {

	private float spawnRate = 1.0f;
	private int spawnTotal = 100;
	private int spawned = 0;

	private GameObject enemy;
	private Path path;

	public void Start() {
		enemy = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.Enemy);

		path = GameObject.Find ("Path").GetComponent<Path>();

		Timing.RunCoroutine (Co_Spawn());
	}

	private IEnumerator<float> Co_Spawn() {
		while(spawned < spawnTotal) {
			Instantiate (enemy).GetComponent<Enemy>().Initialize(path);
			spawned ++;
			yield return Timing.WaitForSeconds(spawnRate);
		}
	}
}
