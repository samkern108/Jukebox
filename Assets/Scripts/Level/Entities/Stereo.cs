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

	private bool deactivated = true;

	public void Initialize(Vector2 position, Pulse pulse) {
		audioSource = GetComponent <AudioSource>();
		anim = GetComponentInChildren <Animator>();
		line = GetComponentInChildren<LineRenderer> ();

		line.startColor = pulse.pulseColor;
		line.endColor = pulse.pulseColor;

		audioSource.clip = ResourceLoader.LoadSFX (pulse.sfxName);

		GetComponent <BoxCollider2D>().size = new Vector2(BeatMaster.beatSize, BeatMaster.beatSize);

		this.transform.position = position;
		this.pulse = pulse;
		this.pulse.position = position;
	}

	public void SetColor(Color color) {
		if (color == Color.white) {
			deactivated = true;
		} else {
			deactivated = false;
			pulse.pulseColor = color;
		
			line.startColor = pulse.pulseColor;
			line.endColor = pulse.pulseColor;
		}
	}

	public void Tick() {
		if (deactivated)
			return;
		
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
}
