using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private float startTime;
	public float spacesPerBeat = 1f;

	private float speed = 8.0f;

	private bool moving = false;

	private Path path;
	private Vector3 targetWaypoint;
	private int waypointCount = 0;

	public void Initialize(Path path) {
		this.path = path;
		this.transform.position = path.GetWaypoint (waypointCount);

		waypointCount++;
		targetWaypoint = path.GetWaypoint (waypointCount);

		speed = BeatMaster.beatSize;

		moving = true;
	}

	void Update () {
		if (moving) {
			MoveAlongPath ();
		}
	}

	private void MoveAlongPath() {
		transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed*Time.deltaTime);

		if(transform.position == targetWaypoint)
		{
			waypointCount++;
			targetWaypoint = path.GetWaypoint (waypointCount);
			//If we've reached the goal, we dieee!
			if (targetWaypoint == transform.position) {
				Destroy (this.gameObject);
			}
		}
	}
}
