using UnityEngine;
using System.Collections;

public abstract class TriggeredTrap : Trap {

	void OnTriggerEnter(Collider collider){

		OnTrapActivated (collider);

	}

	void OnTriggerExit(Collider collider){
		
		OnTrapDeactivated (collider);
		
	}

	public abstract void OnTrapActivated(Collider collider);

	public abstract void OnTrapDeactivated(Collider collider);

}
