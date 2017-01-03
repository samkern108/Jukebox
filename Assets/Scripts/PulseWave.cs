using UnityEngine;
using System.Collections;

public class PulseWave : MonoBehaviour {

	public Pulse pulse;
	public CircleCollider2D circle;
	private float elapsedTime = 0.0f;

	public void Initialize(Pulse pulse) {
		this.pulse = pulse;
		this.transform.position = pulse.position;
	}

	public void Start()
	{
		circle = GetComponent<CircleCollider2D> ();
	}

//	public void OnGUI() {
//		Debug.Log ("ONGUI");
//		Drawing.DrawCircle(transform.position, circle.radius, Color.blue, 0.1f, true, 10);
//	}

	public void Update() {
		elapsedTime += Time.deltaTime;
		circle.radius += Time.deltaTime * this.pulse.speed;
	
		if(circle.radius >= pulse.radius) {
			Debug.Log ("DEAD at " + elapsedTime);
			Destroy(this.gameObject);
		}
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		collider.GetComponent <Dot>().ReactToPulse(pulse);
	}
}
