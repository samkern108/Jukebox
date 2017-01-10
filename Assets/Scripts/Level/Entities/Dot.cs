using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Dot : MonoBehaviour {

	public class PulseTimePair {
		public Pulse pulse;
		public float time;

		public PulseTimePair(Pulse pulse, float time) {
			this.pulse = pulse;
			this.time = time;
		}
	}
	
	private SpriteRenderer spriteRenderer;
	private Animator anim;
	private Pulse activePulse;

	private List<PulseTimePair> reactions = new List<PulseTimePair> ();

	private float health = 200.0f;

	public void Start() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent <Animator>();
	}

	public void ReactToPulse(Pulse pulse) {
		reactions.Add (new PulseTimePair(pulse, 0.0f));
		anim.SetTrigger ("Pulse");
	}

	public void Update() {

		for (int i = 0; i < reactions.Count; i++) {

			activePulse = reactions [i].pulse;

			reactions[i].time += Time.deltaTime;

			spriteRenderer.color += activePulse.pulseColor * (activePulse.strength / health);
				
			if (reactions[i].time >= activePulse.lifeTime) {
				reactions.RemoveAt (i);
			}
		}
	}
}
