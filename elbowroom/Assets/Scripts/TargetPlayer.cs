using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class TargetPlayer : MonoBehaviour {

	public GameObject player;

	public float lerpSpeed = 1f;

	float waitEndTime = Mathf.Infinity;

	bool waiting = false;

	void Start () {

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
		transform.up = Vector3.Lerp(transform.up, directionToPlayer, 0.001f*lerpSpeed);

		//constant speed follow
		/*
		Vector3 upDirectionWithEqualDistanceAsDistanceToPlayer = transform.up*distanceToPlayer;

		Vector3 difference = directionToPlayer - upDirectionWithEqualDistanceAsDistanceToPlayer;
		difference.Normalize();

		difference *= 0.003f;

		transform.up += difference;
		*/

		float distanceToPlayer = directionToPlayer.magnitude;
		
		//a raycast's info can be stored in here later
		RaycastHit hit;

		//draw a line from this point
		GetComponent<LineRenderer>().SetPosition(0, transform.position);
		//to this point
		//cast a ray in the direction the turret is facing, and check if you hit the player
		if (Physics.Raycast(transform.position + transform.up*2, transform.up, out hit) && hit.collider.tag == "Player")
		{
			GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.up*distanceToPlayer);

			//shoot after waiting this long
			if (!waiting)
				Wait(1f);
		}
		else
		{
			GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.up*100f);

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

	void Shoot(){
		Debug.Log("pew pew pew");
	}
}
