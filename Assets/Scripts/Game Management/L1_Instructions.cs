using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_Instructions : MonoBehaviour {

	private GameObject arrow, spacebar, mouse, circle;
	private int stage = 0;

	void Start() {
		StereoManager.spawningDisabled = true;

		arrow = transform.FindChild ("Arrow").gameObject;
		spacebar = transform.FindChild ("Spacebar").gameObject;
		mouse = transform.FindChild ("Mouse").gameObject;
		circle = transform.FindChild ("SelectionCircle").gameObject;

		arrow.SetActive (false);
		spacebar.SetActive (false);
		mouse.SetActive (false);
		circle.SetActive (false);
		Invoke("NextStage", 2f);
	}

	public void NextStage() {
		switch (stage) {
		case 0: 
			circle.SetActive (true);
			mouse.SetActive (true);
			break;
		case 1:
			StereoManager.InstantiateStereo (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			circle.SetActive (false);
			mouse.SetActive (false);
			Invoke ("NextStage", 4f);
			break;
		case 2:
			StereoManager.spawningDisabled = false;
			arrow.SetActive (true);
			spacebar.SetActive (true);
			break;
		}
		stage++;
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			BeatMaster.gameStarted = true;
			Destroy (this.gameObject);
		}
	}
}
