using UnityEngine;
using System.Collections;

public class EddyController : MonoBehaviour {

	public GameObject feet;

	public float moveSpeed = 13;

	float accumulatedVelocity = 0;

	float originalHeight;

	void Start () {
		originalHeight = transform.position.y;
	}

	void Update () {

		TiltCharacter ();

	}

	void FixedUpdate(){

		DetectInput ();

		if (Input.GetKey (KeyCode.W))
		{
			Bounce ();
		}

	}

	void DetectInput(){

		if (Input.GetKeyDown(KeyCode.W) ||Input.GetKeyDown(KeyCode.S) )
			accumulatedVelocity = 0;

		if (Input.GetKey (KeyCode.W)) {
			//set velocity, so no acceleration
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x,0,moveSpeed);
		}
		if (Input.GetKey (KeyCode.S)) {

			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x,0,-moveSpeed);
		}

		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody>().velocity += new Vector3(-1,0,0);
		}
		if (Input.GetKey (KeyCode.D)) {
			//accelerate right by adding velocity each frame
			GetComponent<Rigidbody>().velocity += new Vector3(1,0,0);
		}

		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S)) {
			//when you let go of a key, set velocity to almost zero, then the player will seem to suddenly almost stop
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x,0,GetComponent<Rigidbody>().velocity.z/3);
		}

	}

	void Bounce(){

		accumulatedVelocity += GetComponent<Rigidbody>().velocity.z;
		//add a sine wave to the players height depending on how fast they are going forward or backward
		transform.position = new Vector3(transform.position.x,originalHeight + Mathf.Abs(Mathf.Sin(accumulatedVelocity*2f))/2f,transform.position.z);
	}

	void TiltCharacter(){
		Quaternion newRotation = transform.rotation;
		//set the z of the player's rotation depending on how fast the player is going left or right: the x velocity
		newRotation.z = -GetComponent<Rigidbody>().velocity.x/50;
		transform.rotation = newRotation;
	}
}
