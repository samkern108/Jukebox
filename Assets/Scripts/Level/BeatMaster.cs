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

	//SLOPPY! Instead, set the beat game object to be active when the game starts.
	public static bool gameStarted = false;

	AudioSource metronome;

	void Start() {
		metronome = GetComponent <AudioSource>();

		height = 2f * Camera.main.orthographicSize;
		width = height * Camera.main.aspect;

		beatSize = (int)Mathf.Ceil(width/(beatsAcrossWidth));

		beatsAcrossHeight = (int)Mathf.Ceil(height/beatSize);

		BeatGrid.DrawBeatGrid();
		GameObject.Find ("Path").GetComponent<Path>().InitializePath();
	}

	void FixedUpdate() {
		if(gameStarted) {

		timer += Time.fixedDeltaTime;
		if (timer + Time.fixedDeltaTime <= timeBetweenBeats) {
			Invoke ("SendTick", timeBetweenBeats-timer);
			timer = (timer - timeBetweenBeats);
		}
		}
	}
		
	private void SendTick() {
		BroadcastMessage("Tick");
		metronome.Play ();
	}
}
