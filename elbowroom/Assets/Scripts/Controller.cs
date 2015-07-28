using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//Vector3 forward;

	Animator myAnimator;

	void Start () {
		//forward = new Vector3 (0, 0, 20);

		myAnimator = GetComponent<Animator>();
	}

	void Update () {
		if (Input.GetKey (KeyCode.W)) 
		{
			GetComponent<Rigidbody>().AddForce(transform.forward*20);
			myAnimator.SetBool("Walking", true);
		}
		else
		{
			myAnimator.SetBool("Walking", false);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			GetComponent<Rigidbody>().AddForce(-transform.forward*20);

		}

		if (Input.GetKey (KeyCode.A)) 
		{
			myAnimator.SetFloat("Direction", -1);

			transform.Rotate(-Vector3.up*2);
		}
		else if (Input.GetKey (KeyCode.D)) 
		{
			myAnimator.SetFloat("Direction", 1);

			transform.Rotate(Vector3.up*2);
		}
		else
		{
			myAnimator.SetFloat("Direction", 0);
		}
	}
}
