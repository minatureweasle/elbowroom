using UnityEngine;
using System.Collections;

public abstract class TriggeredTrap : Trap {

	bool trapIsActive = false;

	public bool triggeredByDistance = false;

	public float triggerDistance = 5;

	Transform players;

	void OnTriggerEnter(Collider collider){

		OnTrapActivated (collider);
		trapIsActive = true;
	}

	void OnTriggerExit(Collider collider){

		trapIsActive = false;
		OnTrapDeactivated (collider);
		
	}

	void Start(){
		players = GameObject.Find ("Players").transform;
	}

	void Update(){
		if (trapIsActive) {
			OnTrapActive();
		}

		//go through the players and see if any is close enough to activate the trap,
		//but only if the trap isn't already active
		if (!trapIsActive && triggeredByDistance) {
			for (int i = 0; i < players.childCount; i++) {
				if ((players.GetChild (i).transform.position - transform.position).magnitude < triggerDistance)
					OnTrapActivated (collider);
			}
		}
	}

	public abstract void OnTrapActivated(Collider collider);

	//do this the whole time the trap is active
	public abstract void OnTrapActive();

	public abstract void OnTrapDeactivated(Collider collider);

}
