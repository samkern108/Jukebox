using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public void OnEnable()
	{
		LevelMaster.SendPauseNotification (true);
	}

	public void OnDisable()
	{
		LevelMaster.SendPauseNotification (false);
	}

	public void AdjustSFXVolume(float volume)
	{
	}

	public void ContinueLevel()
	{
		this.gameObject.SetActive (false);
	}

	public void RestartLevel() {
		LevelMaster.SendRestartNotification ();
	}

	public void QuitToMenu() {
		LevelMaster.QuitToMenu ();
	}
}
