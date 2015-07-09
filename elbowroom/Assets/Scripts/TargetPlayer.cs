using UnityEngine;
using System.Collections;

public class TargetPlayer : MonoBehaviour {

	public GameObject player;

	Vector3 originalUp;

	void Start () {
		originalUp = transform.up;
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

		//delayed follow
		transform.up = Vector3.Lerp(transform.up, directionToPlayer, 0.001f);

		Ray ray = new Ray(transform.position, transform.up);


		GetComponent<LineRenderer>().SetPosition(0, transform.position);

		GetComponent<LineRenderer>().SetPosition(1, transform.position + transform.up*100f);


	}
}
