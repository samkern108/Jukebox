using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StereoManager : MonoBehaviour {

	public static List<Stereo> stereos = new List<Stereo>();

	public static GameObject pulseObject;

	public void Start() {
		pulseObject = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.PulseWave);
	}

	public void InstantiateStereo(Vector2 clickPosition) {
		GameObject stereo = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.Stereo);
		GameObject stereoClone = Instantiate (stereo);
		stereoClone.GetComponent <Stereo>().Initialize(clickPosition);
	}

	public void Update() {
		if (Input.GetMouseButtonDown (0)) {
			InstantiateStereo (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}
	}
}
