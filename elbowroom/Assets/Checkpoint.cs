﻿using UnityEngine;
using System.Collections;

public class Checkpoint : TriggeredTrap {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTrapTriggered(Collider c){
		if (c.tag == "Player") {
			PlayerStates s = c.transform.GetComponent<PlayerStates>();
			s.setStartPoint(transform.position);
		}
	}
	
	public override void OnTrapDeactivated(Collider c){

	}
}
