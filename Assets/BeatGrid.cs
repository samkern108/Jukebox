using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGrid : MonoBehaviour {

	private static LineRenderer line;

	public void Awake() {
		line = GetComponent <LineRenderer>();
	}

	public static void DrawBeatGrid(float beat) {
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
}
