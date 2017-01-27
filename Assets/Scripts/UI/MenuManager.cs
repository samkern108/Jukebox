using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public void OnEnable()
	{
		LevelMaster.PauseGame (true);
	}

	public void OnDisable()
	{
		LevelMaster.PauseGame (false);
	}

	public void AdjustSFXVolume(float volume)
	{
		SFXManager.volume = volume;
	}

	public void ContinueLevel()
	{
		this.gameObject.SetActive (false);
	}

	public void RestartLevel() {
		LevelMaster.RestartLevel ();
	}

	public void QuitToMenu() {
		LevelMaster.QuitToMenu ();
	}
}
