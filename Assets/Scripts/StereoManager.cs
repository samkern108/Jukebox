using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StereoManager : MonoBehaviour {

	public static List<Stereo> stereos = new List<Stereo>();

	public static GameObject pulseObject;

	public void Start() {
		pulseObject = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.PulseWave);

		InstantiateStereo (new Vector2(5,5));
	}

	public void InstantiateStereo(Vector2 clickPosition) {
		GameObject stereo = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.Stereo);
		GameObject stereoClone = Instantiate (stereo);
		stereoClone.GetComponent <Stereo>().Initialize(clickPosition);
	}
}
