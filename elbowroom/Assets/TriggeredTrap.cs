using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class TriggeredTrap : Trap {

	void OnTriggerEnter(Collider collider){

		TriggerTrap ();

	}

	void TriggerTrap(){

		myAnimation.Play ();

	}

}
