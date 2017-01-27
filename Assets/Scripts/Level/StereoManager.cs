using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pulse {

	public float radius;
	public float speed;
	public float strength;
	public int beatsBetweenPulses;
	public Color pulseColor;
	public string sfxName;

	// Used by Dot
	public Vector2 position;
	public float lifeTime;

	public Pulse(SFXInstrument instr) {
		this.radius = .5f * BeatMaster.beatSize;
		this.speed = 0f;
		this.strength = 0f;
		this.beatsBetweenPulses = 0;
		this.pulseColor = Color.white;
		string name = ResourceLoader.GetRandomSFXName(instr);

		// TODO(samkern): Probably should make this less janky.
		string[] substring = name.Split ('/');

		this.sfxName = substring[substring.Length - 2] + "/" + substring[substring.Length - 1];
		//removing .wav extension
		this.sfxName = sfxName.Substring (0, sfxName.Length - 4);
		Debug.Log (this.sfxName);

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
		Pulse pulse = new Pulse (SFXInstrument.Guitar);

		Stereo stereo = stereoClone.GetComponent <Stereo> ();
		stereo.Initialize(clickPosition, pulse);
		StereoEditorPanel.EditorModeOn (stereo);
		return stereo;
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
					StereoEditorPanel.EditorModeOn (selectedStereo);
				} else if (!spawningDisabled) {
					/*Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					float x = BeatMaster.beatSize * Mathf.Floor(mousePos.x/BeatMaster.beatSize) + BeatMaster.beatSize/2;
					float y = BeatMaster.beatSize * Mathf.Floor(mousePos.y/BeatMaster.beatSize) + BeatMaster.beatSize/2;
					stereoPositionOnGrid = new Vector2(x, y);*/

					selectedStereo = InstantiateStereo (stereoPositionOnGrid);
				}
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
