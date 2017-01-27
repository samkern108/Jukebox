using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Stereo : MonoBehaviour {

	private Pulse pulse;
	private Animator anim;
	private LineRenderer line;

	private AudioSource audioSource;
	private AudioDistortionFilter audioDistortionFilter;
	private AudioLowPassFilter audioLowPassFilter;
	private AudioClip[] audioClips = new AudioClip[3];

	private int beatCounter = 0;
	private int numBeats = 4;
	public int[] beatValues;

	private bool deactivated = true;

	public void Initialize(Vector2 position, Pulse pulse) {
		name = "Stereo" + pulse.sfxName;

		beatValues = new int[numBeats];

		audioSource = GetComponent <AudioSource>();
		audioLowPassFilter = GetComponent <AudioLowPassFilter>();
		audioDistortionFilter = GetComponent <AudioDistortionFilter>();

		anim = GetComponentInChildren <Animator>();
		line = GetComponentInChildren <LineRenderer> ();

		SetColor (pulse.pulseColor);

		transform.localScale *= (BeatMaster.beatSize/3);

		float width = BeatMaster.beatSize * 0.1f;

		line.startWidth = width;
		line.endWidth = width;

		for (int i = 0; i < audioClips.Length; i++) {
			audioClips[i] = ResourceLoader.LoadSFX (pulse.sfxName + (i + 3));
		}

		GetComponent <BoxCollider2D>().size = new Vector2(BeatMaster.beatSize, BeatMaster.beatSize);

		this.transform.position = position;
		this.pulse = pulse;
		this.pulse.position = position;
	}

	public void SetColor(Color color) {
		if (color == Color.white)
			deactivated = true;
		else
			deactivated = false;

		color.a = .6f;
		if(pulse != null) pulse.pulseColor = color;
		line.startColor = color;

		color.a = .3f;
		line.endColor = color;
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
			audioSource.clip = audioClips[beatValues[beatCounter] - 1];
			PlayAudio (beatValues[beatCounter]);
		}

		beatCounter = (beatCounter + 1)%numBeats;
	}

	public void PlayAudio(int beatValue) {
		audioSource.volume = .60f + (beatValue * .10f);
		audioLowPassFilter.cutoffFrequency = beatValue * 2000;
		audioDistortionFilter.distortionLevel = (beatValue * .25f);
		audioSource.Play ();
	}
}
