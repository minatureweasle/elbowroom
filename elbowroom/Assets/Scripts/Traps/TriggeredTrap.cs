using UnityEngine;
using System.Collections;

public abstract class TriggeredTrap : Trap {

	public bool triggeredByDistance = false;
	public float triggerDistance = 5;

	bool trapIsActive = false;

	void Update(){
		if (trapIsActive)
			OnTrapActive ();
		else {
			if (triggeredByDistance) {
				if (PlayerGroup.instance.IsAnyPlayerWithinRange(transform.position, triggerDistance)){
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

    public virtual void OnTrapActivated(Collider collider) { }

    public virtual void OnTrapActive() { }

    public virtual void OnTrapDeactivated(Collider collider) { }

}
