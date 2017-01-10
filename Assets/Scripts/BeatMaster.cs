using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class BeatMaster : MonoBehaviour {

	public static float timeBetweenBeats = 1.0f;
	public static int beatsAcrossWidth = 12;
	public static int beatsAcrossHeight;
	public static float width, height;
	private static float timer = 0.0f;
	public static float beatSize;

	AudioSource metronome;

	void Start() {
		metronome = GetComponent <AudioSource>();

		height = 2f * Camera.main.orthographicSize;
		width = height * Camera.main.aspect;

		beatSize = (int)Mathf.Ceil(width/beatsAcrossWidth);

		beatsAcrossHeight = (int)Mathf.Ceil(height/beatSize);

		BeatGrid.DrawBeatGrid(timeBetweenBeats);
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
