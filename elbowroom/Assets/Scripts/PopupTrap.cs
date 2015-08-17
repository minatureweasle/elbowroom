using UnityEngine;
using System.Collections;

public class PopupTrap : TriggeredTrap {

	public GameObject popup;

	public override void OnTrapTriggered(Collider collider){

		popup.GetComponent<Animation> ().Play ();

	}

	public override void OnTrapDeactivated(Collider collider){
		
	}

}
