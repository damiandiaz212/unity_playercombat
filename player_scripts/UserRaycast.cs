using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * UserRaycast.cs class
 * Created by Damian Diaz
 * Modified 5/18/17 
 * 
 **/
public class UserRaycast : MonoBehaviour {

	private float longDistance;
	public string tagCheck;

	User user;
	SphereCollider col;

	public List<GameObject> enemies;

	void Start()
	{
		user = GetComponent<User> ();
		col = GetComponent<SphereCollider> ();
		longDistance = user.SwingDistance;
		tagCheck = user.TagCheck;
	}

	void Update()
	{

		Debug.DrawRay(new Vector3(transform.position.x,transform.position.y + 1,transform.position.z), transform.forward * longDistance, Color.green);

		for (int i = 0; i < enemies.Count; i++) {
			if (enemies [i] == null) {
				enemies.RemoveAt (i);
			}
		}
	}

	//only call Attack(float) in animation event!!
	public void Attack ()
	{

		RaycastHit[] hits;
		RaycastHit hitUse = new RaycastHit ();

		Vector3 attackLine = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);

		bool foundHit = false;

		hits = Physics.RaycastAll (attackLine, transform.forward  * longDistance);

		for (int i = 0; i < hits.Length; i++) {

			if (hits [i].transform.tag == tagCheck) {
			
				if (Vector3.Distance (transform.position, hits [i].point) <= longDistance) {
					hitUse = hits [i];
					foundHit = true;
				}
				if (foundHit)
					hits [i].transform.SendMessage ("recieve", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	bool whithinSight(GameObject obj)
	{
		Vector3 dirFromAtoB = (obj.transform.position - transform.position).normalized;
		float dotProd = Vector3.Dot(dirFromAtoB, transform.forward);

		if (dotProd > 0.9) 
			return true;
		else
			return false;
	}

	public void SpinAttack()
	{
		if (enemies.Count != 0) {

			for(int i = 0; i < enemies.Count; i++)
				enemies[i].SendMessage ("recieve", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (!enemies.Contains(col.gameObject))
		{
			enemies.Add(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (enemies.Contains(col.gameObject))
		{
			enemies.Remove(col.gameObject);
		}
	}
}