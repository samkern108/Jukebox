using UnityEngine;
using System.Collections;

public class PulseWave : MonoBehaviour {

	private Pulse pulse;
	private CircleCollider2D circle;
	private LineRenderer line;
	private Color lineColor;

	private float elapsedTime = 0.0f;

	public void Initialize(Pulse pulse, int intensity) {
		circle = GetComponent<CircleCollider2D> ();
		line = GetComponent <LineRenderer>();

		this.pulse = pulse;
		this.transform.position = pulse.position;

		circle.radius = (this.pulse.radius - .1f) * intensity;

		lineColor = this.pulse.pulseColor;
		lineColor.a = 0;
		line.startColor = lineColor;
		line.endColor = lineColor;
	}

	private int numPositions = 80;

	public void Update() {
		elapsedTime += Time.deltaTime;

		circle.radius += Time.deltaTime;

		if (elapsedTime <= .3f) {
			lineColor.a += Time.deltaTime * 4;
		} else {
			lineColor.a -= Time.deltaTime;
		}

		line.startColor = lineColor;
		line.endColor = lineColor;

		line.DrawCircle (circle.radius, numPositions);
	
		if(elapsedTime >= 0.8f) {
			Destroy(this.gameObject);
		}
	}


	//TODO this is for growing the pulse out of the center

	/*
	public void Update() {
		elapsedTime += Time.deltaTime;
		circle.radius += Time.deltaTime * this.pulse.speed;

		line.DrawCircle (circle.radius, numPositions);
	
		if(circle.radius >= pulse.radius) {
			Destroy(this.gameObject);
		}
	}*/

	public void OnTriggerEnter2D(Collider2D collider) {
		collider.GetComponent <Enemy>().ReactToPulse(pulse);
	}
}
