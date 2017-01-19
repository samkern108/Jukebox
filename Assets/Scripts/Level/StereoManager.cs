﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pulse {

	public float radius;
	public float speed;
	public float strength;
	public int beatsBetweenPulses;
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
		this.beatsBetweenPulses = p.beatsBetweenPulses;
		this.pulseColor = p.pulseColor;
		this.sfxName = p.sfxName;

		this.lifeTime = this.radius/this.speed;
	}

	public Pulse() {
		this.radius = 1.5f * BeatMaster.beatSize;
		this.speed = 0f;
		this.strength = 0f;
		this.beatsBetweenPulses = 0;
		this.pulseColor = Color.white;
		this.sfxName = ResourceLoader.ResourceNameAudioClip.Distorted1;

		this.lifeTime = 0f;
	}

	public Pulse(float radius, float speed, float strength, int beatsBetween, Color color, ResourceLoader.ResourceNameAudioClip sfxName) {
		this.radius = radius;
		this.speed = speed;
		this.strength = strength;
		this.beatsBetweenPulses = beatsBetween;
		this.pulseColor = color;
		this.sfxName = sfxName;

		this.lifeTime = this.radius/this.speed;
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
	private Transform editStereoPanel;

	private static bool placeStereoMode = true, editStereoMode = false;

	public void Start() {
		self = this;
		p_pulseWave = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.PulseWave);
		p_stereoShadow = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.StereoShadow);

		stereoParent = GameObject.Find ("Stereos").transform;
		editStereoPanel = GameObject.Find ("EditStereoPanel").transform;

		stereoShadow = Instantiate (p_stereoShadow);
		stereoShadowRadius = stereoShadow.transform.FindChild ("Radius").gameObject;

		stereoLineRenderer = stereoShadow.GetComponent <LineRenderer> ();
		stereoRadiusLineRenderer = stereoShadowRadius.GetComponent <LineRenderer> ();

		DrawStereoShadow (false);
	}

	public static Stereo InstantiateStereo(Vector2 clickPosition) {
		GameObject p_stereo = ResourceLoader.LoadPrefab (ResourceLoader.ResourceNamePrefab.Stereo);
		GameObject stereoClone = Instantiate (p_stereo);
		stereoClone.transform.SetParent (stereoParent);
		stereoClone.GetComponent <Stereo>().Initialize(clickPosition, new Pulse());
		return stereoClone.GetComponent <Stereo> ();
	}

	private Stereo selectedStereo;

	void Update () {
		if (editStereoMode) {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("EditStereoOption"));

				if (hit.collider) {
					Color color = hit.collider.gameObject.GetComponent<SpriteRenderer> ().color;
					selectedStereo.SetColor (color);
				}
				editStereoMode = false;
				selectedStereo = null;
				editStereoPanel.position = Vector3.zero;
			}
		}
		else if (placeStereoMode) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Stereo"));

			if(!hit.collider)
				DrawStereoOnMouse ();
			
			if (Input.GetMouseButtonDown (0)) {
				//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				//RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Stereo"));

				if (hit.collider) {
					selectedStereo = hit.collider.gameObject.GetComponent<Stereo>();
					editStereoPanel.position = selectedStereo.transform.position;
				} else {
					/*Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					float x = BeatMaster.beatSize * Mathf.Floor(mousePos.x/BeatMaster.beatSize) + BeatMaster.beatSize/2;
					float y = BeatMaster.beatSize * Mathf.Floor(mousePos.y/BeatMaster.beatSize) + BeatMaster.beatSize/2;
					stereoPositionOnGrid = new Vector2(x, y);*/

					selectedStereo = InstantiateStereo (stereoPositionOnGrid);
					editStereoPanel.position = selectedStereo.transform.position;
				}
				editStereoMode = true;
			}
		}
	}

	public void DrawStereoShadow(bool draw) {
		stereoShadow.SetActive (draw);
	}

	private int numPositions = 80;
	private Vector2 stereoPositionOnGrid;
	private void DrawStereoOnMouse() {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float x = BeatMaster.beatSize * Mathf.Floor(mousePos.x/BeatMaster.beatSize) + BeatMaster.beatSize/2;
		float y = BeatMaster.beatSize * Mathf.Floor(mousePos.y/BeatMaster.beatSize) + BeatMaster.beatSize/2;
		stereoPositionOnGrid = new Vector2(x, y);
		stereoShadow.transform.position = stereoPositionOnGrid;
	}
}
