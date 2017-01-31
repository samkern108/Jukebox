using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_LevelLoader : MonoBehaviour {

	//The game scene ID.
	private int levelID = 1;

	public void LoadLevel(string levelName) {
		SceneManager.LoadScene (levelID);
	}
}
