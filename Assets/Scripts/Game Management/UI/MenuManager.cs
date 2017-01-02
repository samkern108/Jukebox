using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public void OnEnable()
	{
		GameManager.PauseGame (true);
	}

	public void OnDisable()
	{
		GameManager.PauseGame (false);
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
