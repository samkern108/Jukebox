using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour {

	public static int enemiesRemaining, livesRemaining;
	private LevelJSON level;

	public static bool paused = false;

	void Start () {
		level = IOManager.LoadLevel ("One");
		livesRemaining = level.lives;
		UIManager.self.SetLivesRemainingUI (level.lives);
		BeatMaster.InitializeBeat (level.grid);
		StereoManager.InitializeStereosTemplates (level.stereos);
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

		StereoManager.self.DrawStereoShadow (!pause);
	}

	public static void RestartLevel() {
		Debug.Log ("Restarting.");
	}

	public static void QuitToMenu() {
		Application.Quit ();
	}
}
