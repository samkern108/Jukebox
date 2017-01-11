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

	public void AdjustMusicVolume(float volume)
	{
		BGMusicManager.AdjustVolume (volume);
	}

	public void AdjustSFXVolume(float volume)
	{
		SFXManager.volume = volume;
	}

	public void ContinueLevel()
	{
		this.gameObject.SetActive (false);
	}
}
