using UnityEngine;
using System.Collections;

public abstract class PulseWave : MonoBehaviour {

	protected Pulse pulse;
	protected LineRenderer line;
	protected Color lineColor;

	protected float elapsedTime = 0.0f;

	public virtual void Initialize(Pulse pulse, int intensity) {
		this.pulse = pulse;
		this.transform.position = pulse.position;
		line = GetComponent <LineRenderer>();

		lineColor = this.pulse.pulseColor;
		lineColor.a = 0;
		line.startColor = lineColor;
		line.endColor = lineColor;

		float width = BeatMaster.beatSize * 0.1f + (intensity + 1)/8;

		line.startWidth = width;
		line.endWidth = width;
	}

	public virtual void Update() {
		if (!paused) {
			elapsedTime += Time.deltaTime;

			// Why 0.8f...? @_@
			if (elapsedTime >= 0.8f) {
				Destroy (this.gameObject);
			}
		}
	}

	public abstract void OnTriggerEnter2D (Collider2D collider);

	protected bool paused = false;
	public void Pause(bool pause) {
		paused = pause;
	}
}
