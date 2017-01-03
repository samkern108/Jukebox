using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float amp = 0.0f;
	public float ampNeeded = 20.0f;

	private float startTime;
	private float speed = 0.02f;

	private bool moving = false;

	private Transform start, goal;

	public void Initialize(Transform start, Transform goal) {
		this.start = start;
		this.goal = goal;
		this.transform.position = start.position;
		startTime = Time.time;
		moving = true;
	}

	void Update () {
		if (moving) {
			this.transform.position = Vector2.Lerp (start.position, goal.position, (speed * (Time.time - startTime)));
		}
	}
}
