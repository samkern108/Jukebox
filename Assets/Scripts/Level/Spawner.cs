﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Spawner : MonoBehaviour {

	private int spawnTotal = 100;
	private int spawned = 0;

	private Transform enemyParent;

	private GameObject enemy;
	private Path path;

	public void Start() {
		enemy = ResourceLoader.LoadPrefab (ResourceNamePrefab.Enemy);

		enemyParent = GameObject.Find ("Enemies").transform;
		path = GameObject.Find ("Path").GetComponent<Path>();

		// TODO(samkern): This is hacky.
		LevelMaster.enemiesRemaining = spawnTotal;
	}

	public void Tick() {
		if(spawned < spawnTotal) {
			GameObject enemyInstance = Instantiate (enemy);
			enemyInstance.GetComponent<Enemy>().Initialize(path);
			enemyInstance.transform.SetParent (enemyParent);
			spawned ++;
			LevelMaster.EnemySpawned ();
		}
	}
}
