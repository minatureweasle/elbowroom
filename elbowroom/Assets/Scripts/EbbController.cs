using UnityEngine;
using System.Collections;

public class EbbController : MonoBehaviour {


	Animator myAnimator;

	Rigidbody myRigidbody;

	Vector3 wantedVelocity;

	//public float strafeSpeed = 10;

	public bool faceWhereGoing = true;

	public float gravityMultiplier = 4f;

	public float jumpVelocity = 15f;

	void Start () {

		myAnimator = GetComponent<Animator> ();

		myRigidbody = GetComponent<Rigidbody> ();

		Physics.gravity = Physics.gravity * gravityMultiplier;
	}

	void Update () {

		if (Input.GetKey (KeyCode.W)) 
		{
			//GetComponent<Rigidbody>().AddForce(transform.forward*20);
			AccelerateFromToZ(3, 18, 10f*Time.deltaTime);
			myAnimator.SetBool("Walking", true);
			myAnimator.SetFloat("Speed", wantedVelocity.z);
		}
		else if (Input.GetKey (KeyCode.S)) 
		{
			//GetComponent<Rigidbody>().AddForce(-transform.forward*20);
			AccelerateFromToZ(-3, -18, -20f*Time.deltaTime);
			myAnimator.SetBool("Walking", true);
			myAnimator.SetFloat("Speed", Mathf.Abs(wantedVelocity.z));
		}
		else
		{
			myAnimator.SetBool("Walking", false);

			wantedVelocity.z = 0;
			myAnimator.SetFloat("Speed", 0);

			Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
			newVelocity.z = 0;
			GetComponent<Rigidbody>().velocity = newVelocity;
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			//myAnimator.SetFloat("Direction", -1);
			/*Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
			newVelocity.x = -strafeSpeed;
			GetComponent<Rigidbody>().velocity = newVelocity;*/

			//GetComponent<Rigidbody>().AddForce(new Vector3(-1*strafeSpeed, 0, 0));
			AccelerateFromToX(-6, -12, -30f*Time.deltaTime);

		}
		else if (Input.GetKey (KeyCode.D)) 
		{
			//myAnimator.SetFloat("Direction", 1);
			/*Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
			newVelocity.x = strafeSpeed;
			GetComponent<Rigidbody>().velocity = newVelocity;*/

			//GetComponent<Rigidbody>().AddForce(new Vector3(1*strafeSpeed, 0, 0));
			AccelerateFromToX(6, 12, 30f*Time.deltaTime);
		}
		else
		{
			wantedVelocity.x = 0;


			Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
			newVelocity.x = 0;
			GetComponent<Rigidbody>().velocity = newVelocity;
		}

		if (Input.GetKeyDown (KeyCode.Space) &! myAnimator.GetBool("Jumping"))
			Jump ();

		if (faceWhereGoing)
			transform.forward = wantedVelocity;

	}

	void OnCollisionEnter(Collision collision){

		if (collision.transform.name == "Floor") {

			myAnimator.SetBool("Jumping", false);

		}

	}

	void Jump(){

		Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
		newVelocity.y = jumpVelocity;
		GetComponent<Rigidbody>().velocity = newVelocity;

		myAnimator.SetBool("Jumping", true);




	}

	void AccelerateFromToX(float initialVelocity, float maxVelocity, float acceleration){

		//Debug.Log ("wantedvelocity x: "+wantedVelocity.x+ "   rigidbody velocity: "+myRigidbody.velocity);

		Vector3 newVelocity = myRigidbody.velocity;

		//start at the initial velocity
		if (Mathf.Abs (wantedVelocity.x) < Mathf.Abs (initialVelocity)) {
			wantedVelocity.x = initialVelocity;
		}
		//accelerate up to the max
		else if (Mathf.Abs (wantedVelocity.x) < Mathf.Abs (maxVelocity))
			wantedVelocity.x += acceleration;
		//if youre greater or equal to the max, stay at the max
		else
			wantedVelocity.x = maxVelocity;

		newVelocity.x = wantedVelocity.x;

		myRigidbody.velocity = newVelocity;

	}

	void AccelerateFromToZ(float initialVelocity, float maxVelocity, float acceleration){
		
		//Debug.Log ("wantedvelocity x: "+wantedVelocity.x+ "   rigidbody velocity: "+myRigidbody.velocity);
		
		Vector3 newVelocity = myRigidbody.velocity;
		
		//start at the initial velocity
		if (Mathf.Abs (wantedVelocity.z) < Mathf.Abs (initialVelocity)) {
			wantedVelocity.z = initialVelocity;
		}
		//accelerate up to the max
		else if (Mathf.Abs (wantedVelocity.z) < Mathf.Abs (maxVelocity))
			wantedVelocity.z += acceleration;
		//if youre greater or equal to the max, stay at the max
		else
			wantedVelocity.z = maxVelocity;
		
		newVelocity.z = wantedVelocity.z;
		
		myRigidbody.velocity = newVelocity;
		
	}
}
