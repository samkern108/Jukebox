using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class BeatMaster : MonoBehaviour {

	float timeBetweenBeats = 1.0f;
	float timer = 0.0f;

	AudioSource metronome;

	void Start() {
		metronome = GetComponent <AudioSource>();
	}

	void Update () {
		timer += Time.deltaTime;

		if(timer >= timeBetweenBeats) {
			BroadcastMessage("Tick");
			metronome.Play ();
			timer -= timeBetweenBeats;
		}
	}
}
