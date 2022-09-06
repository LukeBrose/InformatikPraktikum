using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController2D controller;

	public float runSpeed = 14f;
	public float maxacc = 200;
	public float acceleration = 1;

	public float horizontalMove = 0f;
	bool jump = false;

	// Update is called once per frame
	void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * (acceleration * 0.01f);

		if (Input.GetAxisRaw("Horizontal") != 0)
		{
			if (acceleration < maxacc)
				acceleration = acceleration + 1;
		}

		//Reset Acceleration
		else
		{
			acceleration = 1;
		}


		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}


	}
	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}

}

