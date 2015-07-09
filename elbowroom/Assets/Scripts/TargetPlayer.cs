using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class TargetPlayer : MonoBehaviour {

	public GameObject player;

	public float lerpSpeed = 1f;

	void Start () {

	}

	void Update () {

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


		float distanceToPlayer = directionToPlayer.magnitude;

		//constant speed follow
		/*
		Vector3 upDirectionWithEqualDistanceAsDistanceToPlayer = transform.up*distanceToPlayer;

		Vector3 difference = directionToPlayer - upDirectionWithEqualDistanceAsDistanceToPlayer;
		difference.Normalize();

		difference *= 0.003f;

		transform.up += difference;
		*/


		
		//cast a ray in the direction the turret is facing
		RaycastHit hit;

		//draw a line from this point
		GetComponent<LineRenderer>().SetPosition(0, transform.position);
		//to this point
		if (Physics.Raycast(transform.position + transform.up*2, transform.up, out hit) && hit.collider.tag == "Player")
			GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.up*distanceToPlayer);
		else
			GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.up*100f);


	}
}
