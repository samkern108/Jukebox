using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			BeatMaster.gameStarted = true;
			Destroy (this.gameObject);
		}
	}
}
