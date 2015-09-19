using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	Animator myAnimator;

    //cache the Animator
	void Start () {
		myAnimator = GetComponentInChildren<Animator> ();
	}

    //play the animation that opens the door, when the player gets close
	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player")
			myAnimator.Play("OpenDoor");
	}

    //play the animation that closes the door, when the player goes away
	void OnTriggerExit(Collider collider){
		if (collider.tag == "Player")
			myAnimator.Play("CloseDoor");
	}
}
