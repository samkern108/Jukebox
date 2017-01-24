using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Stereo : MonoBehaviour {

	private Pulse pulse;
	private AudioSource audioSource;
	private AudioDistortionFilter audioDistortionFilter;
	private AudioLowPassFilter audioLowPassFilter;
	private Animator anim;
	private LineRenderer line;

	public int[] beatValues;
	private int beatCounter = 0;
	private int numBeats = 4;

	private bool deactivated = true;

	public void Initialize(Vector2 position, Pulse pulse) {
		audioSource = GetComponent <AudioSource>();
		audioLowPassFilter = GetComponent <AudioLowPassFilter>();
		audioDistortionFilter = GetComponent <AudioDistortionFilter>();

		anim = GetComponentInChildren <Animator>();
		line = GetComponentInChildren<LineRenderer> ();

		line.startColor = pulse.pulseColor;
		line.endColor = pulse.pulseColor;

		beatValues = new int[numBeats];

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

	public void SetBeatValue(int beat, int value) {
		beatValues [beat] = value;
	}

	public void Tick() {
		if (deactivated)
			return;

		anim.SetTrigger ("Pulse");
		
		if (beatValues[beatCounter] != 0) {
			GameObject pulseWave = Instantiate (StereoManager.p_pulseWave);
			pulseWave.GetComponent<PulseWave> ().Initialize (pulse, beatValues[beatCounter]);
			pulseWave.transform.SetParent (transform);
			PlayAudio (beatValues[beatCounter]);
		}

		beatCounter = (beatCounter + 1)%numBeats;
	}

	public void PlayAudio(int beatValue) {
		audioLowPassFilter.cutoffFrequency = beatValue * 2000;
		audioDistortionFilter.distortionLevel = (beatValue * .25f);
		audioSource.Play ();
	}
}
