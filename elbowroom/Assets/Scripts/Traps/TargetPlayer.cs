using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class TargetPlayer : MonoBehaviour {

	public GameObject player;

	public float lerpSpeed = 1f;

	public float turretShootingDelay = 0.1f;

	float waitEndTime = Mathf.Infinity;

	bool waiting = false;

	public GameObject bulletPrefab;

	public Transform firepoint;

	public float maxDistance = 30;

	//Transform players;

	void Start () {
		//players = GameObject.Find ("Players").transform;
	}

	void Shoot(){
		//Debug.Log("pew pew pew");
		Instantiate(bulletPrefab, firepoint.position, transform.rotation);

		CancelWait();
	}

	void Update () {

		if (Time.time > waitEndTime)
			Shoot();

		//from turret (this) to player
		Vector3 directionToPlayer = player.transform.position - transform.position;

		//from turret to player
		Debug.DrawRay(transform.position, directionToPlayer, Color.red);

		//currently facing
		Debug.DrawRay(transform.position, transform.up, Color.cyan);


		//perfect follow
		//transform.up = directionToPlayer;

		//slowing down follow
		if (directionToPlayer.magnitude < maxDistance && directionToPlayer.z < 0) {
			transform.up = Vector3.Lerp (transform.up, directionToPlayer, 0.001f * lerpSpeed);
		} 
		else {
			transform.up = Vector3.Lerp (transform.up, Vector3.up, 0.01f * lerpSpeed);
		}
		
		//constant speed follow
		/*
		Vector3 upDirectionWithEqualDistanceAsDistanceToPlayer = transform.up*distanceToPlayer;

		Vector3 difference = directionToPlayer - upDirectionWithEqualDistanceAsDistanceToPlayer;
		difference.Normalize();

		difference *= 0.003f;

		transform.up += difference;
		*/

		//float distanceToPlayer = directionToPlayer.magnitude;
		
		//a raycast's info can be stored in here later
		RaycastHit hit;

		//draw a line from this point
		GetComponent<LineRenderer>().SetPosition(0, transform.position);
		//to this point
		//cast a ray in the direction the turret is facing, and check if you hit the player
		if (Physics.Raycast(firepoint.position, transform.up, out hit) && hit.collider.name != "Plane")
		{


			//Debug.Log("name:"+hit.collider.name);

			float distanceToHit = (hit.collider.transform.position - transform.position).magnitude;

			GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.up.normalized*distanceToHit);


			//shoot after waiting 1 second, 
			//but not if you've already started waiting, 
			//and only if the thing to shoot is close enough
			//and only if you are behind the turret, not ahead
			if (hit.collider.tag == "Player" && !waiting && distanceToHit < maxDistance && hit.point.z < transform.position.z)
				Wait(turretShootingDelay);
		}
		else //nothing was hit
		{
			//draw no line
			GetComponent<LineRenderer>().SetPosition(1, transform.position);

			//no longer targeting the player, so stop getting ready to shoot
			CancelWait();
		}


	}

	void CancelWait(){
		waiting = false;

		waitEndTime = Mathf.Infinity;
	}

	void Wait(float duration){
		waiting = true;

		waitEndTime = Time.time + duration;
	}


}
