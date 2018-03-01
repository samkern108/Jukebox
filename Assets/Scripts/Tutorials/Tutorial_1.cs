using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_1 : Tutorial {

	void Start() {
		Invoke("NextStage", 0.5f);
	}

	public void NextStage() {
		switch (stage) {
		case 0: 
			RectTransform circleTransform = icon_selectionCircle.GetComponent<RectTransform>();
			RectTransform CanvasRect = GetComponent<Canvas>().GetComponent<RectTransform>();

			Vector2 ViewportPosition=Camera.main.WorldToViewportPoint(BeatMaster.GetClosestGridCellCenter(6, 3));
			Vector2 WorldObject_ScreenPosition=new Vector2(
				((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x * 0.5f)),
				((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y * 0.5f)));

			circleTransform.anchoredPosition = WorldObject_ScreenPosition;

			icon_selectionCircle.SetActive (true);
			icon_mouse.SetActive (true);
			break;
		case 1:
			StereoManager.InstantiateStereo (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			icon_selectionCircle.SetActive (false);
			icon_mouse.SetActive (false);
			Invoke ("NextStage", 2f);
			break;
		case 2:
			icon_arrow.SetActive (true);
			icon_spacebar.SetActive (true);
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
