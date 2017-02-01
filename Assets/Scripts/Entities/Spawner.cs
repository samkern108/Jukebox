using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Spawner : MonoBehaviour {

	private int spawnTotal, spawned = 0;
	private int beatsBetweenSpawns, countdown;

	private Transform enemyParent;

	private GameObject enemy;
	private Path path;

	public void InitializeSpawner(Path path, SpawnerJSON spawner) {
		this.path = path;

		transform.SetParent (GameObject.Find("BeatMaster").transform, false);
		transform.position = Vector3.zero;

		enemy = ResourceLoader.LoadPrefab (ResourceNamePrefab.Enemy);
		enemyParent = GameObject.Find ("Enemies").transform;

		beatsBetweenSpawns = spawner.beatsBetweenSpawn;
		countdown = beatsBetweenSpawns;

		spawnTotal = spawner.numEnemies;
		LevelMaster.enemiesTotal += spawnTotal;
		UIManager.self.SetEnemiesRemainingUI(LevelMaster.enemiesTotal);
	}

	public void Tick(int beat) {
		if (countdown <= 0) {
			if (spawned < spawnTotal) {
				GameObject enemyInstance = Instantiate (enemy);
				enemyInstance.GetComponent<Enemy> ().Initialize (path);
				enemyInstance.transform.SetParent (enemyParent);
				spawned++;
				LevelMaster.EnemySpawned ();
			}
			countdown = beatsBetweenSpawns;
		}
		else
			countdown--;
	}

	public void InitLevel() {
		spawned = 0;
		countdown = 0;
	}
}
