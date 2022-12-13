﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;            // The speed that the player will move at.

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	PlayerEffect playerEffect;
	public bool IsSuper;

	void Awake () {
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		
		// Set up references.
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
		playerEffect = GetComponent<PlayerEffect>();
		IsSuper = false;
	}
	
	
	void FixedUpdate () {
		// Store the input axes.			
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Move the player around the scene.
		Move (h, v);
		
		// Turn the player to face the mouse cursor.
		Turning ();
		
		// Animate the player.
		Animating (h, v);
	}
	
	
	void Move (float h, float v) {
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}

	// Create a ray from the mouse cursor on screen in the direction of the camera.
	Ray camRay;

	// Create a RaycastHit variable to store information about what was hit by the ray.
	RaycastHit floorHit;
	void Turning () {
		camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;
			
			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;
			
			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);
			
			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotatation);
		}
	}

    void Animating (float h, float v) {
		// Create a boolean that is true if either of the input axes is non-zero.
		bool walking = h != 0f || v != 0f;
		
		// Tell the animator whether or not the player is walking.
		anim.SetBool ("IsWalking", walking);
	}

	public void UpgradeSpeed() 
	{
		speed += 1;
		playerEffect.PlayUpgradeSpeed();
		if (speed > 20) speed = 20;
	}

	public void SuperUpgradeSpeed()
    {
		StartCoroutine(IEUpgradeSuperSpeed());
    }

	IEnumerator IEUpgradeSuperSpeed()
    {
		float tempSpeed = speed;
		speed = 20f;
		IsSuper = true;
		yield return new WaitForSeconds(5f);
		speed = tempSpeed;
		IsSuper = false;
    }
		
}