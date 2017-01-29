using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Animator anim;

	/*Health determines the number of rings the dot has.
	 * The inner ring gets filled up first. The outer ring second, obviously.
	 * The hard part is that the second and third rings only get filled up when the dot ONLY has that other color in previous dots.
	 */
	private int health = 1;

	private float startTime;
	public float spacesPerBeat = 1f;

	private float speed = 8.0f;

	private bool moving = false;

	private Path path;
	private Vector3 targetWaypoint;
	private int waypointCount = 0;

	public void Initialize(Path path) {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent <Animator>();

		this.path = path;
		this.transform.position = path.GetWaypoint (waypointCount);

		waypointCount++;
		targetWaypoint = path.GetWaypoint (waypointCount);

		speed = BeatMaster.beatSize;

		moving = true;
	}

	void Update () {
		if (moving)
			MoveAlongPath ();
	}

	private void MoveAlongPath() {
		transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed*Time.deltaTime);

		if(transform.position == targetWaypoint)
		{
			waypointCount++;
			targetWaypoint = path.GetWaypoint (waypointCount);
			//If we've reached the goal, we dieee!
			if (targetWaypoint == transform.position) {
				if(spriteRenderer.color != path.endColor)
					LevelMaster.EnemyDied ();
				
				Destroy (this.gameObject);
			}
		}
	}

	public void ReactToPulse(Pulse pulse) {
		//anim.SetTrigger ("Pulse");
		spriteRenderer.color = ColorHelper.CombineColors (spriteRenderer.color, pulse.pulseColor);
	}
}