using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager self;
	private static GameObject menu;

	public void Awake()
	{
		self = this;
		menu = transform.FindChild ("Menu").gameObject;
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
		transform.FindChild ("VictoryPanel").gameObject.SetActive (true);
	}

	public void ShowDefeatPanel()
	{
		transform.FindChild ("GameOverPanel").gameObject.SetActive (true);
	}

	public void SetEnemiesRemainingUI(int remaining) {
		GameObject.Find ("EnemiesRemaining").GetComponent<Text>().text = remaining + "";
	}

	public void SetLivesRemainingUI(int remaining) {
		GameObject.Find ("LivesRemaining").GetComponent<Text>().text = remaining + "";
	}

	private void HideAllUI() {
		transform.FindChild ("VictoryPanel").gameObject.SetActive (false);
		transform.FindChild ("GameOverPanel").gameObject.SetActive (false);
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
