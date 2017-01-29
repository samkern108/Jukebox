using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	// Camera should always have its left middle edge at 0,0. (right now its bottom-left corner is centered at (0,0))
	public void Start() {
		transform.position = new Vector3(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize, -10);
	}
}
