using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_LevelLoader : MonoBehaviour {

	public void LoadLevel(int levelID) {
		SceneManager.LoadScene (levelID);
	}
}
