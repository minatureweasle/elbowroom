using UnityEngine;
using System.Collections;

public class PopupTrap : TriggeredTrap {

	public GameObject popup;

    //Play the popup's animation when the trap is activated
	public override void OnTrapActivated(Collider collider){
		popup.GetComponent<Animation> ().Play ();
	}

}
