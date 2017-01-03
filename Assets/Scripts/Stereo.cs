using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public struct Pulse {

	public float radius;
	public float speed;
	public float strength;
	public float timeBetweenPulses;
	public Color pulseColor;

	// Used by Dot
	public Vector2 position;
	public float lifeTime;

	// ONLY used by Dot
	public float dotElapsedTime;

	public Pulse(float radius, float speed, float strength, float time, Color color, Vector2 position) {
		this.radius = radius;
		this.speed = speed;
		this.strength = strength;
		this.timeBetweenPulses = time;
		this.pulseColor = color;

		this.dotElapsedTime = 0.0f;
		this.lifeTime = this.radius/this.speed;
		this.position = position;
	}
}

public class Stereo : MonoBehaviour {

	public Pulse pulse;

	public void Initialize(Vector2 position) {
		this.transform.position = position;
		pulse = new Pulse (10.0f, 5.0f, 5.0f, 5.0f, Color.red, position);
		StartPulses ();
		//Invoke ("StartPulses", pulse.timeBetweenPulses);
	}

	private void StartPulses() {
		Timing.RunCoroutine (Co_EmitPulses());
	}
		
	private IEnumerator<float> Co_EmitPulses() {
		while (true) {
			EmitPulse ();
			yield return Timing.WaitForSeconds(pulse.timeBetweenPulses);
		}
	}	

	public void EmitPulse () {
		GameObject pulseWave = Instantiate (StereoManager.pulseObject);
		pulseWave.GetComponent<PulseWave>().Initialize (pulse);

		//Grid.HandlePulse (pulse, this.transform.position);
	}
}
