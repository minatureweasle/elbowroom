using UnityEngine;
using System.Collections;

public abstract class TriggeredTrap : Trap {

	void OnTriggerEnter(Collider collider){

		OnTrapTriggered (collider);

	}

	void OnTriggerExit(Collider collider){
		
		OnTrapDeactivated (collider);
		
	}

	public abstract void OnTrapTriggered(Collider collider);

	public abstract void OnTrapDeactivated(Collider collider);

}
