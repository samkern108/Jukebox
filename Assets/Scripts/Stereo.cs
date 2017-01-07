using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Stereo : MonoBehaviour {

	private Pulse pulse;
	private AudioSource audio;

	public void Initialize(Vector2 position, Pulse pulse) {
		Debug.Log (pulse.sfxName);
		audio = GetComponent <AudioSource>();
		audio.clip = ResourceLoader.LoadSFX (pulse.sfxName);
		Debug.Log (audio.clip);

		GetComponent <LineRenderer>().startColor = pulse.pulseColor;
		GetComponent <LineRenderer>().endColor = pulse.pulseColor;
		this.transform.position = position;
		this.pulse = pulse;
		this.pulse.position = position;
	}

	public void Tick() {
		GameObject pulseWave = Instantiate (StereoManager.p_pulseWave);
		pulseWave.GetComponent<PulseWave>().Initialize (pulse);
		pulseWave.transform.SetParent (transform);
		audio.Play ();
	}
}
