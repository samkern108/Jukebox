using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Stereo : MonoBehaviour {

	private Pulse pulse;
	private AudioSource audio;
	private Animator anim;
	private LineRenderer line;

	private bool mouseOn = true;

	public void Initialize(Vector2 position, Pulse pulse) {
		audio = GetComponent <AudioSource>();
		anim = GetComponentInChildren <Animator>();
		audio.clip = ResourceLoader.LoadSFX (pulse.sfxName);
		line = GetComponentInChildren<LineRenderer> ();

		line.startColor = pulse.pulseColor;
		line.endColor = pulse.pulseColor;
		this.transform.position = position;
		this.pulse = pulse;
		this.pulse.position = position;
	}

	public void Tick() {
		GameObject pulseWave = Instantiate (StereoManager.p_pulseWave);
		pulseWave.GetComponent<PulseWave>().Initialize (pulse);
		pulseWave.transform.SetParent (transform);
		//audio.Play ();
		anim.SetTrigger ("Pulse");
	}

	public void OnMouseEnter() {
		Debug.Log ("enter");
		if (!mouseOn) {
			Debug.Log ("trigger");
			line.widthMultiplier = 1.5f;
			line.startWidth = .3f;
			line.endWidth = .3f;
			mouseOn = true;
		}
	}

	public void OnMouseExit() {
		Debug.Log ("exit");
		line.startWidth = .2f;
		line.endWidth = .2f;
		mouseOn = false;
	}
}
