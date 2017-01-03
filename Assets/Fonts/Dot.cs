using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Dot : MonoBehaviour {
	
	SpriteRenderer spriteRenderer;
	private Color baseColor = Color.black;
	private Color adjustedColor = Color.black;

	private List<Pulse> reactions = new List<Pulse> ();

	public void Start() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void ReactToPulse(Pulse pulse) {
		reactions.Add (pulse);
	}

	private Pulse activePulse;

	public void Update() {

		adjustedColor = baseColor;

		for (int i = 0; i < reactions.Count; i++) {

			activePulse = reactions [i];

			activePulse.dotElapsedTime += Time.deltaTime;

			//First Curve
			if (activePulse.dotElapsedTime <= activePulse.lifeTime/2) {
				adjustedColor += activePulse.pulseColor * (activePulse.dotElapsedTime / activePulse.lifeTime) * 2;
			}
			//Second Curve
			else {
				adjustedColor += activePulse.pulseColor * Mathf.Abs((1 - ((activePulse.dotElapsedTime/2) / activePulse.lifeTime) * 2));	
			}

			reactions [i] = activePulse;

			if (activePulse.dotElapsedTime >= activePulse.lifeTime) {
				reactions.RemoveAt (i);
			}
		}

		spriteRenderer.color = adjustedColor;
	}
}
