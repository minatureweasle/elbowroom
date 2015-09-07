using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RocketLauncher : MonoBehaviour {

	public GameObject player;
	public GameObject bulletPrefab;
	public Transform firepoint;

	public float lerpFactor = 1f;

	bool waiting = false;
	float waitEndTime = Mathf.Infinity;
	public float turretShootingDelay = 0.1f;

	public float maxDistance = 30;

	void Update () {
		TargetPlayer ();
		if (Time.time > waitEndTime)
			Shoot();
	}

	//the launcher turns to point at the player and when the player is in sight it schedules rockets to be fired
	void TargetPlayer(){
		Vector3 directionToPlayer = player.transform.position - transform.position;
		
		//point launcher towards player
		if (directionToPlayer.magnitude < maxDistance && directionToPlayer.z < 0)
			transform.up = Vector3.Lerp (transform.up, directionToPlayer, 0.001f * lerpFactor);
		else
			transform.up = Vector3.Lerp (transform.up, Vector3.up, 0.01f * lerpFactor);
		
		//a raycast's info can be stored in here later
		RaycastHit hit;
		
		GetComponent<LineRenderer>().SetPosition(0, transform.position);
		
		//cast a ray in the direction the turret is facing and check if it hits the player
		if (Physics.Raycast(firepoint.position, transform.up, out hit) && hit.collider.name != "Plane")
		{
			float distanceToHit = (hit.collider.transform.position - transform.position).magnitude;
			GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.up.normalized*distanceToHit);
			
			//shoot after waiting 1 second, 
			//but not if you've already started waiting, 
			//and only if the thing to shoot is close enough
			//and only if you are behind the turret, not ahead
			if (hit.collider.tag == "Player" && !waiting && distanceToHit < maxDistance && hit.point.z < transform.position.z)
				WaitToShoot(turretShootingDelay);
		}
		else
		{
			GetComponent<LineRenderer>().SetPosition(1, transform.position);
			StopWaitingToShoot();
		}
	}

	void Shoot(){
		Instantiate(bulletPrefab, firepoint.position, transform.rotation);
		StopWaitingToShoot();
	}

	void StopWaitingToShoot(){
		waiting = false;
		waitEndTime = Mathf.Infinity;
	}

	void WaitToShoot(float duration){
		waiting = true;
		waitEndTime = Time.time + duration;
	}

}
