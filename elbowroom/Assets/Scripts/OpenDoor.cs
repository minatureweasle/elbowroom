using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	Animator myAnimator;

	void Start () {
		myAnimator = GetComponentInChildren<Animator> ();
	}

	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){

		if (collider.tag == "Player") {
			myAnimator.Play("OpenDoor");
		}
	}

	void OnTriggerExit(Collider collider){
		
		if (collider.tag == "Player") {
			myAnimator.Play("CloseDoor");
		}
		
	}
}
