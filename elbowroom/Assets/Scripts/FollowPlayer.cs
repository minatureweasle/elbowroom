﻿using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject playerToFollow;
	public int panSpeed = 2;

    //Checks the players position, and if the player is far from the camera, it will pan towards the player
	void Update () {
		transform.position = new Vector3(transform.position.x, transform.position.y, playerToFollow.transform.position.z - 8);

		if (Mathf.Abs (playerToFollow.transform.position.x - transform.position.x) > 3) {
			Pan ();
		} else if (Mathf.Abs (playerToFollow.transform.position.x - transform.position.x) < 1) {
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x /3,0,GetComponent<Rigidbody>().velocity.z);
		}

	}

    //The camera changes its velocity so that it moves towards the player
	void Pan(){
		if (transform.position.x < playerToFollow.transform.position.x){
			GetComponent<Rigidbody>().velocity += new Vector3(1,0,0);
		}
		else if (transform.position.x > playerToFollow.transform.position.x){
			GetComponent<Rigidbody>().velocity -= new Vector3(1,0,0);
		}
	}
}