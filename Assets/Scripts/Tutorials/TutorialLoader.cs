using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLoader: MonoBehaviour {

	public static Tutorial activeTutorial;

	void Start () {
		Tutorial.LoadResources ();
	}
}
