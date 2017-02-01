using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMaster : MonoBehaviour {

	public static int enemiesTotal, enemiesRemaining, enemiesLeftToSpawn, livesRemaining;
	public static LevelJSON level;

	public static bool paused = false;
	private static string levelToLoad;

	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		IOManager.Initialize ();
	}

	public void LoadLevel(string levelName) {
		levelToLoad = levelName;
		SceneManager.LoadScene (1);
	}

	public static void OkToLoad() {
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
		SendRestartNotification ();
	}

	public static void EnemyDied(bool subtractPoints) {
		if (subtractPoints) {
			livesRemaining--;
			UIManager.self.SetLivesRemainingUI (livesRemaining);
			if (livesRemaining <= 0)
				GameOver ();
		}
		enemiesRemaining--;
		if(enemiesRemaining == 0) {
			Victory ();
		}
	}

	public static void EnemySpawned() {
		enemiesLeftToSpawn--;
		UIManager.self.SetEnemiesRemainingUI (enemiesLeftToSpawn);
	}

	private static void GameOver() {
		UIManager.self.ShowDefeatPanel ();
		SendPauseNotification (true);
	}

	private static void Victory() {
		UIManager.self.ShowVictoryPanel ();
		SendPauseNotification (true);
	}

	public static void QuitToMenu() {
		Application.Quit ();
	}

	public static void SendRestartNotification() {
		enemiesRemaining = enemiesTotal;
		enemiesLeftToSpawn = enemiesTotal;
		livesRemaining = level.lives;
		UIManager.self.SetEnemiesRemainingUI(LevelMaster.enemiesTotal);

		Notifications.SendRestartNotification ();
	}

	public static void SendPauseNotification(bool pause) {
		paused = pause;
		Notifications.SendPauseNotification (pause);
	}
}
