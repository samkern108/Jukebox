using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1_Instructions : MonoBehaviour {

	private GameObject arrow, spacebar, mouse, circle;
	private int stage = 0;

	void Start() {
		arrow = transform.FindChild ("Arrow").gameObject;
		spacebar = transform.FindChild ("Spacebar").gameObject;
		circle = transform.FindChild ("SelectionCircle").gameObject;
		mouse = circle.transform.FindChild ("Mouse").gameObject;

		arrow.SetActive (false);
		spacebar.SetActive (false);
		mouse.SetActive (false);
		circle.SetActive (false);

		Invoke("NextStage", 0.5f);
	}

	public void NextStage() {
		switch (stage) {
		case 0: 
			RectTransform circleTransform = circle.GetComponent<RectTransform>();
			RectTransform CanvasRect = GetComponent<Canvas>().GetComponent<RectTransform>();

			Vector2 ViewportPosition=Camera.main.WorldToViewportPoint(BeatMaster.GetClosestGridCell(5, 5));
			Vector2 WorldObject_ScreenPosition=new Vector2(
				((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x * 0.5f)),
				((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y * 0.5f)));

			circleTransform.anchoredPosition = WorldObject_ScreenPosition;

			circle.SetActive (true);
			mouse.SetActive (true);
			break;
		case 1:
			StereoManager.InstantiateStereo (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			circle.SetActive (false);
			mouse.SetActive (false);
			Invoke ("NextStage", 2f);
			break;
		case 2:
			arrow.SetActive (true);
			spacebar.SetActive (true);
			break;
		}
		stage++;
	}
	
	void Update () {
		if(stage == 3) { 
			if (Input.GetKeyDown (KeyCode.Space)) {
				StereoManager.spawningDisabled = false;
				LevelMaster.SendPauseNotification (false);
				Destroy (this.gameObject);
			}
		}
	}
}
