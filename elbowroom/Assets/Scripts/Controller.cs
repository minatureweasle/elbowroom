using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	Vector3 forward;

	Animator myAnimator;

	void Start () {
		forward = new Vector3 (0, 0, 20);

		myAnimator = GetComponent<Animator>();
	}

	void Update () {
		if (Input.GetKey (KeyCode.W)) 
		{
			GetComponent<Rigidbody>().AddForce(forward);
			myAnimator.SetBool("Walking", true);
		}
		else
		{
			myAnimator.SetBool("Walking", false);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			GetComponent<Rigidbody>().AddForce(-forward);

		}
	}
}
