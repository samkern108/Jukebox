using UnityEngine;
using System.Collections;

public class PulseWaveCircle : PulseWave {

	private CircleCollider2D circle;

	public override void Initialize(Pulse pulse, int intensity) {
		base.Initialize (pulse, intensity);

		circle = GetComponent<CircleCollider2D> ();
		circle.radius = (intensity + 1) * (BeatMaster.beatSize/2);
	}

	private int numPositions = 80;

	public override void Update() {
		base.Update ();
		if (!paused) {
			circle.radius += Time.deltaTime;
			pulse.radius = circle.radius;

			if (elapsedTime <= .3f) {
				lineColor.a += Time.deltaTime * 4;
			} else {
				lineColor.a -= Time.deltaTime;
			}

			line.startColor = lineColor;
			line.endColor = lineColor;

			line.DrawCircle (circle.radius, numPositions);
		}
	}

	public override void OnTriggerEnter2D(Collider2D collider) {
		if (Mathf.Abs (Vector2.Distance (collider.transform.position, transform.position) - pulse.radius) <= 1f) {
			collider.GetComponent <Enemy> ().ReactToPulse (pulse);
		}
	}
}
