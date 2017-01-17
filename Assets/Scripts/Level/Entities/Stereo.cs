using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Stereo : MonoBehaviour {

	private Pulse pulse;
	private AudioSource audioSource;
	private Animator anim;
	private LineRenderer line;

	private int beatCountdown = 0;

	//private bool mouseOn = true;

	public void Initialize(Vector2 position, Pulse pulse) {
		audioSource = GetComponent <AudioSource>();
		anim = GetComponentInChildren <Animator>();
		audioSource.clip = ResourceLoader.LoadSFX (pulse.sfxName);
		line = GetComponentInChildren<LineRenderer> ();

		line.startColor = pulse.pulseColor;
		line.endColor = pulse.pulseColor;
		this.transform.position = position;
		this.pulse = pulse;
		this.pulse.position = position;
	}

	public void Tick() {
		if (beatCountdown == 0) {
			beatCountdown = pulse.beatsBetweenPulses;
			GameObject pulseWave = Instantiate (StereoManager.p_pulseWave);
			pulseWave.GetComponent<PulseWave> ().Initialize (pulse);
			pulseWave.transform.SetParent (transform);
			//audio.Play ();
			anim.SetTrigger ("Pulse");
		} else {
			beatCountdown--;
		}
	}

	/*public void OnMouseEnter() {
		if (!mouseOn) {
			mouseOn = true;
		}
	}

	public void OnMouseExit() {
		mouseOn = false;
	}*/
}
