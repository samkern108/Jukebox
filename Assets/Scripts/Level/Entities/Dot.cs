using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Dot : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Animator anim;

	private float resistance = 10.0f;

	public void Start() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent <Animator>();
	}

	public void ReactToPulse(Pulse pulse) {
		//anim.SetTrigger ("Pulse");
		Color newColor = spriteRenderer.color;
		newColor.r = Mathf.Clamp(newColor.r + pulse.pulseColor.r * (pulse.strength / resistance), 0, 1);
		newColor.g = Mathf.Clamp(newColor.g + pulse.pulseColor.g * (pulse.strength / resistance), 0, 1);
		newColor.b = Mathf.Clamp(newColor.b + pulse.pulseColor.b * (pulse.strength / resistance), 0, 1);

		spriteRenderer.color = newColor;

		if(spriteRenderer.color == Color.white) {
			Die ();
		}
	}

	private void Die() {
		Destroy (this.gameObject);
	}
}
