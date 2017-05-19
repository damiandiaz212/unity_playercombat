using UnityEngine;
using System.Collections;

/**
 * UserCombat.cs class
 * Created by Damian Diaz
 * Modified 5/16/17 
 * 
 **/
public class UserCombat : MonoBehaviour
{

	private bool com_isBlocking;
	private bool com_isRunning;
	private bool com_isRolling;
	private bool com_canAttack;
	private bool com_isAttacking;
	private float Distance;
	private bool paused;
	private bool canMAttack = true;
	private bool canSpecial = true;

	Animator anim;
	User user;

	void Start ()
	{
		user = this.GetComponent<User> ();
		anim = this.GetComponent<Animator> ();

	}

	void Update ()
	{

		paused = user.Paused;
		com_isBlocking = user.Blocking;
		com_isRunning = user.Moving;
		com_canAttack = user.CanAttack;
		com_isAttacking = user.IsAttacking;

		if (!paused)
			tAttack ();

	}

	void tAttack ()
	{
		if (com_canAttack) {
			
			if (!com_isRunning) {
				if (Input.GetButtonDown ("Attack1")) {
					
					anim.SetTrigger ("Attack01"); //Attack animation
				} 
			} else {
				if (Input.GetButtonDown ("Attack1")) {
					anim.SetTrigger ("moveAttack");
					user.MoveSpeed = user.MoveSpeed / 1.5f;
					user.CanAttack = false;
					user.IsAttacking = true;
					StartCoroutine (waitToMoveAttack (1.5F));
				}
			}
		}
	}

	IEnumerator waitToMoveAttack (float waitTime) //Cooldown
	{
		yield return new WaitForSeconds (waitTime);
		user.IsAttacking = false;
		user.CanAttack = true;
	}

	public void readyToAttack (int type) //Cooldown
	{
		if (type == 0) {
			user.CanAttack = false;
			user.CanMove = false;
			user.IsAttacking = true;
		} else {
			user.CanAttack = true;
			user.IsAttacking = false;
			user.CanMove = true;
		}

	}
}
