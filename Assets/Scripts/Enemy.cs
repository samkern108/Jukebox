using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float amp = 0.0f;
	public float ampNeeded = 20.0f;

	private float startTime;
	private float speed = 0.2f;

	private bool moving = false;

	private Path path;
	private Transform currentWaypoint, targetWaypoint;
	private int waypointCount = 0;

	public void Initialize(Path path) {
		this.path = path;
		currentWaypoint = path.GetWaypoint (waypointCount);
		this.transform.position = currentWaypoint.position;

		waypointCount++;
		targetWaypoint = path.GetWaypoint (waypointCount);

		startTime = Time.time;
		moving = true;
	}

	void Update () {
		if (moving) {
			MoveAlongPath ();
		}
	}

	private void MoveAlongPath() {
		float percentage = (speed * (Time.time - startTime));
		this.transform.position = Vector2.Lerp (currentWaypoint.position, targetWaypoint.position, percentage);
	
		//make this smoother (can halt)

		if (this.transform.position == targetWaypoint.position) {
			waypointCount++;
			currentWaypoint = targetWaypoint;
			targetWaypoint = path.GetWaypoint (waypointCount);
			startTime = Time.time;
		}
	}
}
