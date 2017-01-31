using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour {

	public static int enemiesRemaining, livesRemaining;
	public static LevelMaster self;
	private LevelJSON level;

	public static string levelToLoad = "Two";

	public static bool paused = false;

	void Start () {
		self = this;
		IOManager.Initialize ();
		level = IOManager.LoadLevel (levelToLoad);
		livesRemaining = level.lives;
		UIManager.self.SetLivesRemainingUI (level.lives);
		BeatMaster.InitializeBeat (level.grid);
		StereoEditorPanel.InitializeStereoColors (level.stereoColors);

		GameObject p_Path = ResourceLoader.LoadPrefab (ResourceNamePrefab.Path);
		Path path;
		foreach (PathJSON pathJSON in level.paths) {
			path = Instantiate (p_Path).GetComponent<Path>();
			path.InitializePath (pathJSON);
			path.transform.position = Vector3.zero;
		}

		SendPauseNotification (true);
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
		SendPauseNotification (true);
	}

	public static void Victory() {
		UIManager.self.ShowVictoryPanel ();
		SendPauseNotification (true);
	}

	public static void QuitToMenu() {
		Application.Quit ();
	}

	public static void SendRestartNotification() {
		self.BroadcastMessage ("InitLevel");
	}

	public static void SendPauseNotification(bool pause) {
		paused = pause;
		self.BroadcastMessage ("Pause", pause);
	}
}
