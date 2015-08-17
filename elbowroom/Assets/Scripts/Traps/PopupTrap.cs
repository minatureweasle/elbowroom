using UnityEngine;
using System.Collections;

public class PopupTrap : TriggeredTrap {

	public GameObject popup;

	public override void OnTrapActivated(Collider collider){

		popup.GetComponent<Animation> ().Play ();

	}

	public override void OnTrapDeactivated(Collider collider){
		
	}

	public override void OnTrapActive(){
	}

}
