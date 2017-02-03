using UnityEngine;
using System.Collections;

public class PulseWaveRect : PulseWave {

	private BoxCollider2D boxCollider;

	public override void Initialize(Pulse pulse, int intensity) {
		base.Initialize (pulse, intensity);
		boxCollider = GetComponent<BoxCollider2D> ();
		float side = BeatMaster.beatSize/2 + 2 * intensity;
		boxCollider.size = new Vector2(side, side);
	}

	public override void Update() {
		base.Update ();
		if (!paused) {

			float time = Time.deltaTime;

			boxCollider.size += new Vector2(time, time);
			// TODO(samkern): Uhh is /2 correct? How do we use radius again?
			pulse.radius = boxCollider.size.x/2;

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
