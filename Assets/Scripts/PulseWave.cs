using UnityEngine;
using System.Collections;

public class PulseWave : MonoBehaviour {

	private Pulse pulse;
	private CircleCollider2D circle;
	private LineRenderer line;

	private float elapsedTime = 0.0f;

	public void Initialize(Pulse pulse) {
		this.pulse = pulse;
		this.transform.position = pulse.position;
	}

	public void Start()
	{
		circle = GetComponent<CircleCollider2D> ();
		line = GetComponent <LineRenderer>();
		line.numPositions = numPositions + 1;
		delta = (2 * Mathf.PI) / numPositions;
	}

	public void Update() {
		elapsedTime += Time.deltaTime;
		circle.radius += Time.deltaTime * this.pulse.speed;
		DrawCircle ();
	
		if(circle.radius >= pulse.radius) {
			Destroy(this.gameObject);
		}
	}

	int numPositions = 40;
	float delta;

	private void DrawCircle()
	{
		float x;
		float y;
		for (int i = 0; i < (numPositions + 1); i++) {

			x = circle.radius * Mathf.Cos (delta * i);
			y = circle.radius * Mathf.Sin (delta * i);
			line.SetPosition (i, transform.position + new Vector3(x, y, 0));
		}



		/*// Calculate each point (theta) in the circle
		// And set its position in the LineRenderer
		int i = 0;
		for(float theta = 0f; theta < (2*Mathf.PI); theta += thetaDelta)
		{
			// Calculate position of point
			float x = (circle.radius*100) * Mathf.Cos(theta);
			float y = (circle.radius*100) * Mathf.Sin(theta);

			// Set the position of this point
			Vector3 pos = new Vector3(x, y, 1);
			line.SetPosition(i, pos);
			i++;
		}*/
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		collider.GetComponent <Dot>().ReactToPulse(pulse);
	}
}
