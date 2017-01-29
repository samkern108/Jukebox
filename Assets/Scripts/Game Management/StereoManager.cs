﻿using UnityEngine;
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
		this.radius = 0;
		this.speed = 0f;
		this.strength = 0f;
		this.beatsBetweenPulses = 0;
		this.pulseColor = Color.white;
		string name = ResourceLoader.GetRandomSFXName(instr);

		// TODO(samkern): Probably should make this less janky.
		string[] substring = name.Split ('/');

		this.sfxName = substring[substring.Length - 2] + "/" + substring[substring.Length - 1];
		//removing .wav extension
		this.sfxName = sfxName.Substring (0, sfxName.Length - 5);

		this.lifeTime = 0f;
	}
}

public class StereoManager : MonoBehaviour {

	public static StereoManager self;

	private static List<Stereo> stereos = new List<Stereo>();

	public static GameObject p_pulseWave;

	private static LineRenderer stereoLineRenderer, stereoRadiusLineRenderer;

	private static Transform stereoParent;

	public static bool spawningDisabled = false;

	public void Awake() {
		self = this;
		p_pulseWave = ResourceLoader.LoadPrefab (ResourceNamePrefab.PulseWave);

		stereoParent = GameObject.Find ("Stereos").transform;
	}

	public static Stereo InstantiateStereo(Vector2 position) 
	{
		float x = BeatMaster.beatSize * Mathf.Floor (position.x / BeatMaster.beatSize) + BeatMaster.beatSize / 2;
		float y = BeatMaster.beatSize * Mathf.Floor (position.y / BeatMaster.beatSize) + BeatMaster.beatSize / 2;

		GameObject p_stereo = ResourceLoader.LoadPrefab (ResourceNamePrefab.Stereo);
		GameObject stereoClone = Instantiate (p_stereo);
		stereoClone.transform.SetParent (stereoParent);
	
		Stereo stereo = stereoClone.GetComponent <Stereo> ();

		Pulse pulse = new Pulse (SFXInstrument.Synth);
		stereo.Initialize(new Vector2(x, y), pulse);

		selectedStereo = stereo;
		StereoEditorPanel.EditorModeOn (stereo);
		return stereo;
	}

	public static Stereo selectedStereo;

	void Update () {
		if (!Input.GetMouseButtonDown (0))
			return;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D hitStereo = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Stereo"));

		// If we touched a stereo, turn editor mode on and return.
		if (hitStereo.collider) {
			selectedStereo = hitStereo.collider.gameObject.GetComponent<Stereo> ();
			StereoEditorPanel.EditorModeOn (selectedStereo);
			return;
		} 

		// If we did not touch a stereo...
		if (StereoEditorPanel.active) {
			RaycastHit2D hitUI = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("UI"));

			//If we did not touch a UI element, turn edit mode off.
			if (!hitUI.collider)
				StereoEditorPanel.EditorModeOff ();
		} 
		// If we aren't in edit mode and we can spawn a stereo, spawn it.
		else if (!spawningDisabled) {
			selectedStereo = InstantiateStereo (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			StereoEditorPanel.EditorModeOn (selectedStereo);
		}
	}
}
