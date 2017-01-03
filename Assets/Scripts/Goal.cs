using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D col) {
		Destroy (col.gameObject);
	}
}
