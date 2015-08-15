using UnityEngine;
using System.Collections;

public class PopupTrap : TriggeredTrap {

	public GameObject popup;

	public override void TriggerTrap(){

		popup.GetComponent<Animation> ().Play ();

	}

	public override void DeactivateTrap(){
		
	}

}
