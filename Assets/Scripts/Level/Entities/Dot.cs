using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Dot : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Animator anim;

	/*Health determines the number of rings the dot has.
	 * The inner ring gets filled up first. The outer ring second, obviously.
	 * The hard part is that the second and third rings only get filled up when the dot ONLY has that other color in previous dots.
	 */
	private int health = 1;

	public void Start() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent <Animator>();
	}

	public void ReactToPulse(Pulse pulse) {
		//anim.SetTrigger ("Pulse");
		spriteRenderer.color = ColorHelper.CombineColors (spriteRenderer.color, pulse.pulseColor);

		if(spriteRenderer.color == Color.white) {
			Die ();
		}
	}

	private void Die() {
		Destroy (this.gameObject);
	}
}
