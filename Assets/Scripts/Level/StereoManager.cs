using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pulse {

	public float radius;
	public float speed;
	public float strength;
	public int beatsBetweenPulses;
	public Color pulseColor;
	public ResourceNameAudioClip sfxName;

	// Used by Dot
	public Vector2 position;
	public float lifeTime;

	public Pulse() {
		this.radius = .5f * BeatMaster.beatSize;
		this.speed = 0f;
		this.strength = 0f;
		this.beatsBetweenPulses = 0;
		this.pulseColor = Color.white;
		this.sfxName = ResourceLoader.GetRandomSFX();

		this.lifeTime = 0f;
	}
}

public class StereoManager : MonoBehaviour {

	public static StereoManager self;

	private static List<Stereo> stereos = new List<Stereo>();

	public static GameObject p_pulseWave;
	private static GameObject p_stereoShadow;
	private static GameObject stereoShadow;
	private static GameObject stereoShadowRadius;

	private static LineRenderer stereoLineRenderer, stereoRadiusLineRenderer;

	private static Transform stereoParent;

	public static bool spawningDisabled = false;

	public void Awake() {
		self = this;
		p_pulseWave = ResourceLoader.LoadPrefab (ResourceNamePrefab.PulseWave);
		p_stereoShadow = ResourceLoader.LoadPrefab (ResourceNamePrefab.StereoShadow);

		stereoParent = GameObject.Find ("Stereos").transform;

		stereoShadow = Instantiate (p_stereoShadow);
		stereoShadowRadius = stereoShadow.transform.FindChild ("Radius").gameObject;

		stereoLineRenderer = stereoShadow.GetComponent <LineRenderer> ();
		stereoRadiusLineRenderer = stereoShadowRadius.GetComponent <LineRenderer> ();

		DrawStereoShadow (false);
	}

	public static Stereo InstantiateStereo(Vector2 clickPosition) {
		GameObject p_stereo = ResourceLoader.LoadPrefab (ResourceNamePrefab.Stereo);
		GameObject stereoClone = Instantiate (p_stereo);
		stereoClone.transform.SetParent (stereoParent);
		Pulse pulse = new Pulse ();
		stereoClone.GetComponent <Stereo>().Initialize(clickPosition, pulse);
		return stereoClone.GetComponent <Stereo> ();
	}

	private Stereo selectedStereo;

	void Update () {
		if (!StereoEditorPanel.active) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Stereo"));

			if(!hit.collider)
				DrawStereoShadowOnMouse ();

			if (Input.GetMouseButtonDown (0)) {
				//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				//RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Stereo"));

				if (hit.collider) {
					selectedStereo = hit.collider.gameObject.GetComponent<Stereo> ();
				} else if (!spawningDisabled) {
					/*Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					float x = BeatMaster.beatSize * Mathf.Floor(mousePos.x/BeatMaster.beatSize) + BeatMaster.beatSize/2;
					float y = BeatMaster.beatSize * Mathf.Floor(mousePos.y/BeatMaster.beatSize) + BeatMaster.beatSize/2;
					stereoPositionOnGrid = new Vector2(x, y);*/

					selectedStereo = InstantiateStereo (stereoPositionOnGrid);
				}
				StereoEditorPanel.EditorModeOn (selectedStereo);
			}
		}
	}

	public void DrawStereoShadow(bool draw) {
		stereoShadow.SetActive (draw);
	}

	private Vector2 stereoPositionOnGrid;

	private void DrawStereoShadowOnMouse() {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float x = BeatMaster.beatSize * Mathf.Floor(mousePos.x/BeatMaster.beatSize) + BeatMaster.beatSize/2;
		float y = BeatMaster.beatSize * Mathf.Floor(mousePos.y/BeatMaster.beatSize) + BeatMaster.beatSize/2;
		stereoPositionOnGrid = new Vector2(x, y);
		stereoShadow.transform.position = stereoPositionOnGrid;
	}
}
