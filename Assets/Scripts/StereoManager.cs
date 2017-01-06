using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pulse {

	public float radius;
	public float speed;
	public float strength;
	public float timeBetweenPulses;
	public Color pulseColor;

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

		this.lifeTime = this.radius/this.speed;
	}

	public Pulse(float radius, float speed, float strength, float time, Color color) {
		this.radius = radius;
		this.speed = speed;
		this.strength = strength;
		this.timeBetweenPulses = time;
		this.pulseColor = color;

		this.lifeTime = this.radius/this.speed;
	}
}

public class StereoManager : MonoBehaviour {

	private static List<Stereo> stereos = new List<Stereo>();
	private static List<Pulse> pulseTemplates = new List<Pulse>();
	private static int selectedPulseTemplate = 0;

	public static GameObject p_pulseWave;
	private static GameObject p_stereoShadow;
	private static GameObject stereoShadow;
	private static GameObject stereoShadowRadius;

	private static LineRenderer stereoLineRenderer, stereoRadiusLineRenderer;

	private static Transform stereoParent;

	public void Start() {
		p_pulseWave = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.PulseWave);
		p_stereoShadow = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.StereoShadow);

		stereoParent = GameObject.Find ("Stereos").transform;

		stereoShadow = Instantiate (p_stereoShadow);
		stereoShadowRadius = stereoShadow.transform.FindChild ("Radius").gameObject;

		stereoLineRenderer = stereoShadow.GetComponent <LineRenderer> ();
		stereoRadiusLineRenderer = stereoShadowRadius.GetComponent <LineRenderer> ();

		InitializePulseTemplates ();
		SelectPulseTemplate (0);
	}

	private void InitializePulseTemplates() {
		pulseTemplates.Add(new Pulse (2.0f, 2.0f, 1.0f, 0.5f, Color.red));
		pulseTemplates.Add(new Pulse (4.0f, 1.0f, 1.0f, 2.0f, Color.blue));
		pulseTemplates.Add(new Pulse (6.0f, 4.0f, 1.0f, 4f, Color.yellow));
	}

	public static void InstantiateStereo(Vector2 clickPosition) {
		GameObject p_stereo = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.Stereo);
		GameObject stereoClone = Instantiate (p_stereo);
		stereoClone.transform.SetParent (stereoParent);
		stereoClone.GetComponent <Stereo>().Initialize(clickPosition, new Pulse(pulseTemplates[selectedPulseTemplate]));
	}

	void Update () {
		DrawStereoOnMouse ();
		if (Input.GetMouseButtonDown (0)) {
			InstantiateStereo (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			SelectPulseTemplate (0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			SelectPulseTemplate (1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			SelectPulseTemplate (2);
		}
	}

	private void SelectPulseTemplate(int template)
	{
		selectedPulseTemplate = template;
		Color color = pulseTemplates [selectedPulseTemplate].pulseColor;
		color.a = .4f;
		stereoLineRenderer.startColor = color;
		stereoLineRenderer.endColor = color;
		stereoRadiusLineRenderer.startColor = color;
		stereoRadiusLineRenderer.endColor = color;
	}

	private int numPositions = 80;
	private void DrawStereoOnMouse() {
		Vector2 newPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		stereoShadow.transform.position = newPos;
		stereoRadiusLineRenderer.DrawCircle (pulseTemplates[selectedPulseTemplate].radius, numPositions);
	}
}
