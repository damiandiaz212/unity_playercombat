using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserHealth : MonoBehaviour {

	public float health;

	public GameObject[] hit;
	public GameObject[] bhit;
	public GameObject UI;
	public AudioClip[] fx;
	//public Transform shieldhit;

	bool alive = true;
	bool blocking = false;
	float cdamage;

	Animator anim;
	User user;

	void Start(){
		anim = GetComponent<Animator> ();
		user = GetComponent<User> ();
		//audio = GetComponent<AudioSource> ();
	}
	void Update(){
		health = user.Health;
	}
	public void ApplyDamageToPlayer(float damage){
		if (alive) {
			if (!blocking) {
				cdamage = damage;
				user.Health -= cdamage;
				Instantiate(hit[Random.Range(0,3)], new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
				int random = Random.Range(0,fx.Length);
				//audio.PlayOneShot(fx[random]);
				Debug.Log ("Dealt "+ cdamage + " damage to Player!" + ", Health: " + health);
			} else {
				//StartCoroutine (WaitBlock (0.3F));
			}
		}
	}
	public void Block(){
		blocking = true;
	}
	public void UnBlock(){
		blocking = false;
	}
	IEnumerator Wait(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		//anim.SetTrigger ("Hit");
		health -= cdamage;
		Instantiate(hit[Random.Range(0,3)], new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
		int random = Random.Range(0,fx.Length);
	//	audio.PlayOneShot(fx[random]);
		Debug.Log ("Dealt "+ cdamage + " damage to Player!" + ", Health: " + health);
	}
	/*
	IEnumerator WaitBlock(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		Instantiate(bhit[Random.Range(0,3)], new Vector3 (shieldhit.position.x, shieldhit.position.y, shieldhit.position.z), Quaternion.identity);
		anim.SetTrigger ("Hit");
	}
	*/
}
