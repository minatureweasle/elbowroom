using UnityEngine;
using System.Collections;

public abstract class TriggeredTrap : Trap {

	void OnTriggerEnter(Collider collider){

		TriggerTrap ();

	}

	void OnTriggerExit(Collider collider){
		
		DeactivateTrap ();
		
	}

	public abstract void TriggerTrap();

	public abstract void DeactivateTrap();

}
