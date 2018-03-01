using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager self;
	private static GameObject menu;

	public void Awake()
	{
		self = this;
		menu = transform.Find ("Menu").gameObject;
		IOManager.Initialize ();
	}

	public void Update()
	{
		//Escape toggles the menu.
		if (Input.GetKeyDown (KeyCode.Escape)) {
			menu.SetActive (!menu.activeInHierarchy);
			LevelMaster.SendPauseNotification (menu.activeInHierarchy);
		}
	}

	public void ShowVictoryPanel()
	{
		transform.Find ("VictoryPanel").gameObject.SetActive (true);
	}

	public void ShowDefeatPanel()
	{
		transform.Find ("GameOverPanel").gameObject.SetActive (true);
	}

	public void SetEnemiesRemainingUI(int remaining) {
		GameObject.Find ("EnemiesRemaining").GetComponent<Text>().text = remaining + "";
	}

	public void SetLivesRemainingUI(int remaining) {
		GameObject.Find ("LivesRemaining").GetComponent<Text>().text = remaining + "";
	}

	private void HideAllUI() {
		transform.Find ("VictoryPanel").gameObject.SetActive (false);
		transform.Find ("GameOverPanel").gameObject.SetActive (false);
		menu.SetActive (false);
	}

	public void InitLevel() {
		HideAllUI ();
	}

	public void RestartLevel() {
		// TODO(samkern): This is a temporary fix to keep the game from getting locked up.
		LevelMaster.SendPauseNotification (false);
		LevelMaster.SendRestartNotification ();
	}

	public void QuitToMenu() {
		LevelMaster.QuitToMenu ();
	}
}
