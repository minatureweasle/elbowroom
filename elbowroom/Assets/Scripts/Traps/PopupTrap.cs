using UnityEngine;
using System.Collections;

public class PopupTrap : TriggeredTrap {

	public GameObject popup;

	public override void OnTrapActivated(Collider collider){
		popup.GetComponent<Animation> ().Play ();
	}

}
