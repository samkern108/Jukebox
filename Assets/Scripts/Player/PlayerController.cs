using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	
	//## CURRENT INPUT VALUES ##//
	private float hAxis = 0, vAxis = 0, hWarp = 0, vWarp = 0;
	private bool sprintButtonDown, jumpButtonDown, jumpButtonUp, warpButtonDown;

	private Vector3 intermediatePosition;
	private Vector3 currentSpeedVector;

	private bool playerInputEnabled = true;

	//## UPDATE ##//
	void Update () 
	{
		if (playerInputEnabled) 
		{
			//1: Conduct Linecasts
			Linecasts ();

			intermediatePosition = transform.position;

			//2: Update Input Values
			vAxis = InputWrapper.GetVerticalAxis ();
			hAxis = InputWrapper.GetHorizontalAxis ();

			//3: Handle Regular Movement
		
			//4: Apply Movement Vectors to Transform

			transform.position = CheckNewPosition (transform.position + currentSpeedVector * Time.deltaTime, transform.position);
		}
	}

	//## RUNNING ##//
	private float runSpeed = 12f, sprintSpeed = 24f;

	//## JUMPING ##//
	private float jumpSpeed = 50f;
	private float jumpArc = 40f;
	private float wallJumpArc = 40f;
	private float lowJumpArc = 30f;
	private float jumpSpeedSprint = 60f;

	//## FALLING ##//
	private float terminalVelocity = -28f, gravityFactor = 170f;

	private Vector3 CheckNewPosition(Vector3 newPos, Vector3 oldPos)
	{
		return newPos;
	}

	// Maybe I can get LinecastNonAlloc to work someday.
	private void Linecasts()
	{
	}

} 