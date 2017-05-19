using UnityEngine;
using System.Collections;

/**
 * User.cs class
 * Created by Damian Diaz
 * Modified 5/16/17 
 * 
 **/
public class User : MonoBehaviour{

	private bool canMove;
	private bool canAttack;
	private bool blocking;
	private bool moveAttacking;
	private bool isMoving;
	private bool isAttacking;
	private string tagCheck;

	[Header ("General Properties")]
	public bool paused;
	public float gravity;
	[Range(0.0f, 100.0f)]
	public float health;

	[Header ("Movement Properties")]
	public float moveSpeed;
	public float evadeTime;
	public float evadeDistance;
	public float cooldown;

	[Header ("Combat Properties")]
	public float swingDistance;

	public User()
	{
		paused = false;
		gravity = 9.8f;
		canMove = true;
		canAttack = true;
		blocking = false;
		moveAttacking = false;
		isMoving = false;
		moveSpeed = 3;
		swingDistance = 1;
		tagCheck = "Enemy";
		health = 100;

	}

	public bool Paused {
		get {
			return paused;
		}
		set {
			paused = value;
		}
	}
	public float Gravity{
		get{return gravity;}
		set{gravity = value;}
	}
	public bool CanMove{
		get {
			return canMove;
		}
		set {
			canMove = value;
		}
	}
	public bool Moving{
		get {
			return isMoving;
		}
		set {
			isMoving = value;
		}
	}
	public bool CanAttack{
		get {
			return canAttack;
		}
		set {
			canAttack = value;
		}
	}
	public bool Blocking{
		get {
			return blocking;
		}
		set {
			blocking = value;
		}
	}
	public bool MoveAttacking{
		get {
			return moveAttacking;
		}
		set {
			moveAttacking = value;
		}
	}
	public bool IsAttacking{
		get {
			return isAttacking;
		}
		set {
			isAttacking = value;
		}
	}
	public float MoveSpeed{
		get {
			return moveSpeed;
		}
		set {
			moveSpeed = value;
		}
	}

	//*************************************************

	public float SwingDistance{
		get {
			return swingDistance;
		}
		set {
			swingDistance = value;
		}
	}
		
	public string TagCheck{
		get {
			return tagCheck;
		}
		set {
			tagCheck = value;
		}
	}
	public float Health{
		get {
			return health;
		}
		set {
			health = value;
		}
	}

}