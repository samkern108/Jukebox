using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	protected int stage;

	protected static GameObject icon_arrow;
	protected static GameObject icon_selectionCircle;
	protected static GameObject icon_spacebar;
	protected static GameObject icon_mouse;

	public static void LoadResources() {
		icon_spacebar = ResourceLoader.LoadIcon(ResourceNameIcon.Spacebar);
		icon_mouse = ResourceLoader.LoadIcon(ResourceNameIcon.Mouse);
		icon_arrow = ResourceLoader.LoadIcon(ResourceNameIcon.Arrow);
		icon_selectionCircle = ResourceLoader.LoadIcon (ResourceNameIcon.SelectionCircle);

		icon_arrow.SetActive (false);
		icon_spacebar.SetActive (false);
		icon_mouse.SetActive (false);
		icon_selectionCircle.SetActive (false);
	}

	void Start () {
		stage = 0;
		LevelMaster.SendPauseNotification (true);
	}

	void Update () {
		
	}
}
