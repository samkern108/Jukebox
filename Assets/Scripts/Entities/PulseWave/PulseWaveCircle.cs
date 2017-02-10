using UnityEngine;
using System.Collections;

public class PulseWaveCircle : PulseWave {

	private CircleCollider2D circle;
	private float circleColliderBuffer;

	public override void Initialize(Pulse pulse, int intensity) {
		base.Initialize (pulse, intensity);

		circle = GetComponent<CircleCollider2D> ();
		pulse.radius = (intensity + 1) * (BeatMaster.beatSize / 2);
		circle.radius = pulse.radius;

		circleColliderBuffer = GetComponent <LineRenderer>().startWidth;
	}

	private int numPositions = 80;

	public override void Update() {
		base.Update ();
		if (!paused) {
			pulse.radius += Time.deltaTime;
			circle.radius = pulse.radius + circleColliderBuffer;

			if (elapsedTime <= .3f) {
				lineColor.a += Time.deltaTime * 4;
			} else {
				lineColor.a -= Time.deltaTime;
			}

			line.startColor = lineColor;
			line.endColor = lineColor;

			line.DrawCircle (pulse.radius, numPositions);
		}
	}

	public override void OnTriggerEnter2D(Collider2D collider) {
		if (Mathf.Abs (Vector2.Distance (collider.transform.position, transform.position) - circle.radius) <= 1f) {
			collider.GetComponent <Enemy> ().ReactToPulse (pulse);
		}
	}
}
