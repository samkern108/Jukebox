using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using MovementEffects;

public class Grid : MonoBehaviour {

	private static List<GameObject> dots = new List<GameObject> ();

	private float xSpacing = 2f;
	private float ySpacing = 2f;
	private float numDots = 1;

	void Start () {
		EstablishGrid ();
	}

	private void EstablishGrid() {
		GameObject dot = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.Dot);
		GameObject dotCopy;

		for (int j = 0; j < 12; j++) {
			for (int i = 0; i < 20; i++) {
				dotCopy = Instantiate (dot);
				dotCopy.transform.position = new Vector2 (i * xSpacing, j * ySpacing);
				dots.Add (dotCopy);
			}
		}
	}

	/*public static void HandlePulse(Pulse pulse, Vector2 position) {
		Timing.RunCoroutine (Co_HandlePulse(pulse, position));	
	}

	private static IEnumerator<float> Co_HandlePulse(Pulse pulse, Vector2 position) {
		float radius = 0.0f;
		// This assumes that dots are evenly spaced.
		float timeBetweenInteractions;
		do {
			yield return Timing.WaitForSeconds();
		} while (radius >= pulse.radius);
	}*/
}
