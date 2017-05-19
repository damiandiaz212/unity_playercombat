using UnityEngine;
using System.Collections;

/**
 * UserMovement.cs class
 * Created by Damian Diaz
 * Modified 5/16/17 
 * 
 **/
public class UserMovement: MonoBehaviour
{
	public bool grounded;

	private float moveSpeed;
	private float currentSpeed;
	private float gravity;
	private float evadeTime;
	private float cooldown;
	private float evadeDistance;

	private float oSpeed;

	//inheritance declerations
	private bool paused;
	private bool canMove;
	private bool blocking;
	private bool isRunning;
	private bool canRoll = true;
	private bool isAttacking;

	//evasion declerations
	private bool evading;
	private bool cooldownInEffect;
	private float evadeTimer;
	private float cooldownTimer;


	CharacterController controller;
	Animator anim;

	User user;
	Vector3 moveDirection;

	public void Start ()
	{
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		user = this.GetComponent<User> ();

		oSpeed = user.MoveSpeed;
	}

	void Update ()
	{
		paused = user.Paused;
		gravity = user.Gravity;
		canMove = user.CanMove;
		blocking = user.Blocking;
		isRunning = user.Moving;
		moveSpeed = user.MoveSpeed;
		isAttacking = user.IsAttacking;
		evadeTime = user.evadeTime;
		evadeDistance = user.evadeDistance;
		cooldown = user.cooldown;

		if (!paused) {
			if (canMove) {
				Movement ();
			} else {
				Movement ();
			}
		}

		grounded = controller.isGrounded;

	}

	void ProcessEvasion ()
	{
		
		cooldownTimer = Mathf.Max (0f, cooldownTimer - Time.deltaTime);

		if ((isRunning && controller.isGrounded) && (canRoll && !isAttacking)) {
			if (!evading && cooldownTimer == 0 && Input.GetButton ("Roll")) {
				evadeTimer = evadeTime;
				anim.SetTrigger ("Roll");
				StartCoroutine (Roll (2));
			
			}
		}

		if (evading) {

			evadeTimer = Mathf.Max (0f, evadeTimer - Time.deltaTime);

			moveSpeed = (evadeDistance / evadeTime); 
			controller.Move (transform.forward * moveSpeed);

			if (evadeTimer == 0) {
				evading = false;

			}
		}

	}

	void Movement ()
	{
		//Movemnent
		Vector3 moveDirection = Vector3.zero;

		if (controller.isGrounded) {
			moveDirection.z = Input.GetAxis ("Vertical");
			moveDirection.x = Input.GetAxis ("Horizontal");

			moveDirection *= moveSpeed;

			if (currentSpeed < 0)
				currentSpeed *= -1;
			 
		}
			
		//Evade
		ProcessEvasion ();

		//Rotation
		if (moveDirection != Vector3.zero) {
			transform.rotation = Quaternion.Slerp (
				transform.rotation,
				Quaternion.LookRotation (moveDirection),
				Time.deltaTime * 10
			);
		}
			

		//Animation
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {
			anim.SetBool ("Run", true);
			user.Moving = true;

		} else {
			anim.SetBool ("Run", false);
			user.Moving = false;
		}
		if (!controller.isGrounded)
			anim.SetBool ("Falling", true);
		else
			anim.SetBool ("Falling", false);

		currentSpeed = moveDirection.magnitude;
		anim.SetFloat ("Speed", currentSpeed);

		moveDirection.y -= gravity;
		controller.Move (moveDirection* Time.deltaTime);
	}

	IEnumerator Roll (float waitTime)
	{
		canRoll = false;
		yield return new WaitForSeconds (waitTime);
		canRoll = true;
	}

	IEnumerator Wait (float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		user.CanMove = true;
	}

	public void resetSpeed ()
	{
		user.MoveSpeed = oSpeed;
	}

	public void Evading()
	{
		evading = true;
	}
	public void pause ()
	{
		if (!paused) {
			user.Paused = true;
			anim.speed = 0;
		} else {
			user.Paused = false;
			anim.speed = 1;
		}
	}
}