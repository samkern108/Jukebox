using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class BeatMaster : MonoBehaviour {

	public static float beatsPerMinute = 80;
	public static float timeBetweenBeats = 60/beatsPerMinute;
	public static int beatsAcrossWidth;
	public static int beatsAcrossHeight;

	public static float beatSize;
	public static int beat = 0;
	public static int beatsPerMeasure = 4;

	private static float width, height;

	private AudioSource metronome;
	private static LineRenderer line;

	public void Awake()
	{
		metronome = GetComponent <AudioSource>();
		line = GetComponent <LineRenderer>();
	}

	public static void InitializeBeat(GridJSON grid)
	{
		height = 2f * Camera.main.orthographicSize;
		width = height * Camera.main.aspect;

		beatsAcrossWidth = grid.w;

		beatSize = (int)Mathf.Ceil(width/(beatsAcrossWidth));

		beatsAcrossHeight = (int)Mathf.Ceil(height/beatSize);

		DrawBeatGrid ();
	}

	private static void DrawBeatGrid() {
		line.numPositions = (BeatMaster.beatsAcrossWidth * 2) + (BeatMaster.beatsAcrossHeight * 2);

		int position = 0;
		float posY = 0;
		float posX = BeatMaster.width;

		for (int i = 0; i < BeatMaster.beatsAcrossWidth; i++) {
			line.SetPosition (position++, new Vector3(i * BeatMaster.beatSize, posY));
			posY = Mathf.Abs (posY - BeatMaster.height);
			line.SetPosition (position++, new Vector3(i * BeatMaster.beatSize, posY));
		}

		for (int j = 0; j < BeatMaster.beatsAcrossHeight; j++) {
			line.SetPosition (position++, new Vector3(posX, j * BeatMaster.beatSize));
			posX = Mathf.Abs (posX - BeatMaster.width);
			line.SetPosition (position++, new Vector3(posX, j * BeatMaster.beatSize));
		}
	}

	public static Vector3 GetClosestGridCellCenter(int x, int y) {
		
		float xPos = (x * BeatMaster.beatSize) + BeatMaster.beatSize/2;
		float yPos = (y * BeatMaster.beatSize) + BeatMaster.beatSize/2;

		return new Vector2(xPos, yPos);
	}

	public static Vector3 GetClosestGridCellBotLeft(int x, int y) {

		float xPos = (x * BeatMaster.beatSize);
		float yPos = (y * BeatMaster.beatSize);

		return new Vector2(xPos, yPos);
	}
		
	//TODO(samkern): There's something fishy going on. It looks like that "Invoke" gets called pretty frequently.
	private void SendTick() {
		BroadcastMessage("Tick", beat);
		metronome.Play ();
		beat = (beat + 1) % beatsPerMeasure;
	}

	public void Pause(bool pause) {
		if (pause)
			CancelInvoke ();
		else
			InvokeRepeating ("SendTick",timeBetweenBeats,timeBetweenBeats);
	}

	public void InitLevel() {
		beat = 0;
	}
}
