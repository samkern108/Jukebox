using UnityEngine;
using System.Collections;

public class PulseWaveRect : PulseWave {

	private BoxCollider2D boxCollider;
	private float boxColliderBuffer;

	public override void Initialize(Pulse pulse, int intensity) {
		base.Initialize (pulse, intensity);
		boxCollider = GetComponent<BoxCollider2D> ();
		pulse.radius = (intensity * 3) * (BeatMaster.beatSize);
		boxCollider.size = new Vector2(pulse.radius, pulse.radius);

		boxColliderBuffer = GetComponent <LineRenderer>().startWidth;
	}

	public override void Update() {
		base.Update ();
		if (!paused) {

			pulse.radius += Time.deltaTime;

			boxCollider.size = new Vector2(pulse.radius + boxColliderBuffer, pulse.radius + boxColliderBuffer);

			if (elapsedTime <= .3f) {
				lineColor.a += Time.deltaTime * 4;
			} else {
				lineColor.a -= Time.deltaTime;
			}

			line.startColor = lineColor;
			line.endColor = lineColor;

			line.DrawBox (boxCollider.size.x);
		}
	}

	public override void OnTriggerEnter2D(Collider2D collider) {
		//if (Mathf.Abs (Vector2.Distance (collider.transform.position, transform.position) - pulse.radius) <= 1f)
			collider.GetComponent <Enemy> ().ReactToPulse (pulse);
	}
}
