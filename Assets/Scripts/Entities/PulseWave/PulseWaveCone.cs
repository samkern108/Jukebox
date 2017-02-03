using UnityEngine;
using System.Collections;

public class PulseWaveCone : PulseWave {

	private PolygonCollider2D coneCollider;

	public override void Initialize(Pulse pulse, int intensity) {
		base.Initialize (pulse, intensity);

		coneCollider = GetComponent<PolygonCollider2D> ();
		ConstructConeCollider ();
	}

	public override void Update() {
		base.Update ();
		if (!paused) {

			if (elapsedTime <= .3f) {
				lineColor.a += Time.deltaTime * 4;
			} else {
				lineColor.a -= Time.deltaTime;
			}

			line.startColor = lineColor;
			line.endColor = lineColor;

			line.DrawCone (coneCollider.points);
		}
	}

	private void ConstructConeCollider() {
		//coneCollider.pathCount = 4;
		coneCollider.points = new Vector2[] { new Vector2(0,0), new Vector2(-1,-1), new Vector2(1,1), new Vector2(0,0) };
	}

	public override void OnTriggerEnter2D(Collider2D collider) {
		//if (Mathf.Abs (Vector2.Distance (collider.transform.position, transform.position) - pulse.radius) <= 1f) {
			collider.GetComponent <Enemy> ().ReactToPulse (pulse);
		//}
	}
}
