using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour {

	public static int startLives = 10;
	public static int enemiesRemaining, livesRemaining;

	public static bool paused = false;

	void Start () {
		livesRemaining = startLives;
		UIManager.self.SetLivesRemainingUI (startLives);
	}

	public static void EnemyDied() {
		livesRemaining--;
		UIManager.self.SetLivesRemainingUI (livesRemaining);

		if (livesRemaining <= 0)
			GameOver ();
	}

	public static void EnemySpawned() {
		enemiesRemaining--;
		UIManager.self.SetEnemiesRemainingUI (enemiesRemaining);
	}

	public static void GameOver() {
		UIManager.self.ShowDefeatPanel ();
	}

	public static void Victory() {
		UIManager.self.ShowVictoryPanel ();
	}

	public static void PauseGame(bool pause)
	{
		paused = pause;
		if (pause) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}

	public static void RestartLevel() {
	}

	public static void QuitToMenu() {
	}
}
