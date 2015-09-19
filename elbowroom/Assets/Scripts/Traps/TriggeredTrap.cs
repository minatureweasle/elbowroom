using UnityEngine;
using System.Collections;

public abstract class TriggeredTrap : Trap {

	public bool triggeredByDistance = false;
	public float triggerDistance = 5;

	bool trapIsActive = false;

    //Activates the trap if any player is close enough and the trap is triggered by distance
    //Calls the OnTrapActive function if the trap is active, every frame
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

    //Activates the trap
	void OnTriggerEnter(Collider collider){
		OnTrapActivated (collider);
		trapIsActive = true;
	}
	
    //Deactivates the trap
	void OnTriggerExit(Collider collider){
		trapIsActive = false;
		OnTrapDeactivated (collider);
	}

    public virtual void OnTrapActivated(Collider collider) { }

    public virtual void OnTrapActive() { }

    public virtual void OnTrapDeactivated(Collider collider) { }

}
