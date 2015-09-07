using UnityEngine;
using System.Collections;

public abstract class TriggeredTrap : Trap {

	public bool triggeredByDistance = false;
	public float triggerDistance = 5;

	bool trapIsActive = false;

	void Update(){
		if (trapIsActive) {
			OnTrapActive ();
		} 
		else {
			if (triggeredByDistance) {
				if (AllPlayers.instance.IsAnyPlayerWithinRange(transform.position, triggerDistance)){
					OnTrapActivated (collider);
					trapIsActive = true;
				}
			}
		}
	}

	void OnTriggerEnter(Collider collider){
		OnTrapActivated (collider);
		trapIsActive = true;
	}
	
	void OnTriggerExit(Collider collider){
		trapIsActive = false;
		OnTrapDeactivated (collider);
	}

	public abstract void OnTrapActivated(Collider collider);

	public abstract void OnTrapActive();

	public abstract void OnTrapDeactivated(Collider collider);

}
