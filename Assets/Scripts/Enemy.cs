using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private float startTime;
	private float speed = 1.0f;

	private bool moving = false;

	private Path path;
	private Transform targetWaypoint;
	private int waypointCount = 0;

	public void Initialize(Path path) {
		this.path = path;
		this.transform.position = path.GetWaypoint (waypointCount).position;

		waypointCount++;
		targetWaypoint = path.GetWaypoint (waypointCount);

		moving = true;
	}

	void Update () {
		if (moving) {
			MoveAlongPath ();
		}
	}

	private void MoveAlongPath() {
		transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed*Time.deltaTime);

		if(transform.position == targetWaypoint.position)
		{
			waypointCount++;
			targetWaypoint = path.GetWaypoint (waypointCount);
		}
	}
}
