using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1_Instructions : MonoBehaviour {

	private GameObject arrow, spacebar, mouse, circle;
	private int stage = 0;

	void Start() {
		StereoManager.spawningDisabled = true;

		arrow = transform.FindChild ("Arrow").gameObject;
		spacebar = transform.FindChild ("Spacebar").gameObject;
		circle = transform.FindChild ("SelectionCircle").gameObject;
		mouse = circle.transform.FindChild ("Mouse").gameObject;

		arrow.SetActive (false);
		spacebar.SetActive (false);
		mouse.SetActive (false);
		circle.SetActive (false);
		Invoke("NextStage", 2f);
	}

	public void NextStage() {
		switch (stage) {
		case 0: 
			//this is the ui element
			RectTransform circleTransform = circle.GetComponent<RectTransform>();

			//first you need the RectTransform component of your canvas
			RectTransform CanvasRect = GetComponent<Canvas>().GetComponent<RectTransform>();

			//then you calculate the position of the UI element
			//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

			Vector2 ViewportPosition=Camera.main.WorldToViewportPoint(BeatMaster.GetClosestGridCell(3, 3));
			Vector2 WorldObject_ScreenPosition=new Vector2(
				((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
				((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));

			//now you can set the position of the ui element
			circleTransform.anchoredPosition = WorldObject_ScreenPosition;

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
