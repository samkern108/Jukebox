using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pulse {

	public float radius;
	public float speed;
	public float strength;
	public float timeBetweenPulses;
	public Color pulseColor;
	public ResourceLoader.ResourceNameAudioClip sfxName;

	// Used by Dot
	public Vector2 position;
	public float lifeTime;

	public Pulse(Pulse p) {
		//lazy man's pass by value
		this.radius = p.radius;
		this.speed = p.speed;
		this.strength = p.strength;
		this.timeBetweenPulses = p.timeBetweenPulses;
		this.pulseColor = p.pulseColor;
		this.sfxName = p.sfxName;

		this.lifeTime = this.radius/this.speed;
	}

	public Pulse(float radius, float speed, float strength, float time, Color color, ResourceLoader.ResourceNameAudioClip sfxName) {
		this.radius = radius;
		this.speed = speed;
		this.strength = strength;
		this.timeBetweenPulses = time;
		this.pulseColor = color;
		this.sfxName = sfxName;

		this.lifeTime = this.radius/this.speed;
	}
}

public class StereoManager : MonoBehaviour {

	public static StereoManager self;

	private static List<Stereo> stereos = new List<Stereo>();
	private static List<Pulse> pulseTemplates = new List<Pulse>();
	private static int selectedPulseTemplate = -1;

	public static GameObject p_pulseWave;
	private static GameObject p_stereoShadow;
	private static GameObject stereoShadow;
	private static GameObject stereoShadowRadius;

	private static LineRenderer stereoLineRenderer, stereoRadiusLineRenderer;

	private static Transform stereoParent;

	private static bool placeStereoMode = false;

	public void Start() {
		self = this;
		p_pulseWave = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.PulseWave);
		p_stereoShadow = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.StereoShadow);

		stereoParent = GameObject.Find ("Stereos").transform;

		stereoShadow = Instantiate (p_stereoShadow);
		stereoShadowRadius = stereoShadow.transform.FindChild ("Radius").gameObject;

		stereoLineRenderer = stereoShadow.GetComponent <LineRenderer> ();
		stereoRadiusLineRenderer = stereoShadowRadius.GetComponent <LineRenderer> ();

		InitializePulseTemplates ();
		DrawStereoShadow (false);
	}

	private void InitializePulseTemplates() {
		pulseTemplates.Add(new Pulse (6.0f, 2.0f, 1.0f, 2.0f, Color.red, ResourceLoader.ResourceNameAudioClip.Strum1));
		pulseTemplates.Add(new Pulse (8.0f, 1.0f, 1.0f, 4.0f, Color.blue, ResourceLoader.ResourceNameAudioClip.Strum2));
		pulseTemplates.Add(new Pulse (12.0f, 4.0f, 1.0f, 8.0f, Color.yellow, ResourceLoader.ResourceNameAudioClip.Strum3));
	}

	public static void InstantiateStereo(Vector2 clickPosition) {
		GameObject p_stereo = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.Stereo);
		GameObject stereoClone = Instantiate (p_stereo);
		stereoClone.transform.SetParent (stereoParent);
		stereoClone.GetComponent <Stereo>().Initialize(clickPosition, new Pulse(pulseTemplates[selectedPulseTemplate]));
	}

	void Update () {
		if (placeStereoMode) {
			DrawStereoOnMouse ();
			if (Input.GetMouseButtonDown (0)) {
				InstantiateStereo (Camera.main.ScreenToWorldPoint (Input.mousePosition));
				placeStereoMode = false;
				DrawStereoShadow (false);
			}
		}
		if (!LevelMaster.paused) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				SelectPulseTemplate (0);
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				SelectPulseTemplate (1);
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				SelectPulseTemplate (2);
			}
		}
	}

	public void DrawStereoShadow(bool draw) {
		stereoShadow.SetActive (draw);
	}

	private void SelectPulseTemplate(int template)
	{
		if (selectedPulseTemplate == template && placeStereoMode) {
			placeStereoMode = false;
			DrawStereoShadow (false);
		} else {
			placeStereoMode = true;
			selectedPulseTemplate = template;
			Color color = pulseTemplates [selectedPulseTemplate].pulseColor;
			color.a = .4f;
			stereoLineRenderer.startColor = color;
			stereoLineRenderer.endColor = color;
			stereoRadiusLineRenderer.startColor = color;
			stereoRadiusLineRenderer.endColor = color;
			DrawStereoOnMouse ();
			DrawStereoShadow (true);
		}
	}

	private int numPositions = 80;
	private void DrawStereoOnMouse() {
		Vector2 newPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		stereoShadow.transform.position = newPos;
		stereoRadiusLineRenderer.DrawCircle (pulseTemplates[selectedPulseTemplate].radius, numPositions);
	}
}
