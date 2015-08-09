using UnityEngine;
using System.Collections;

public class EbbController : MonoBehaviour {

	Animator myAnimator;

	Rigidbody myRigidbody;

	Vector3 targetVelocity;

	public bool faceWhereGoing = true;

	public bool increaseGravityWhenFalling = true;

	public bool canMoveWhileJumping = true;

	public bool stopSuddenly = true;

	public float gravityMultiplier = 4f;

	public float jumpVelocity = 15f;

	public float rollPower = 30;

	public float rollDuration = 0.5f;

	public float jumpTimeOut = 0.7f;

	public float respawnHeight = -25;

	public float strafeMinSpeed = 0;
	public float strafeMaxSpeed = 16;
	public float strafeAcceleration = 30;

	public float forwardMinSpeed = 3;
	public float forwardMaxSpeed = 18;
	public float forwardAcceleration = 10;

	float rollEndTime = Mathf.Infinity;
	float jumpEndTime = Mathf.Infinity;

	public enum PlayerState {IDLE, RUNNING, JUMPING, ROLLING};

	PlayerState myState;

	void Start () {

		myAnimator = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody> ();

		Physics.gravity = Physics.gravity * gravityMultiplier;

		myState = PlayerState.IDLE;
	}

	void Update () {
		//Debug.Log (myState);

		if (Time.time > rollEndTime) {

			myState = PlayerState.RUNNING;

			myAnimator.SetBool ("Rolling", false);

			rollEndTime = Mathf.Infinity;
		}

		if (Time.time > jumpEndTime) {

			myState = PlayerState.RUNNING;

			myAnimator.SetBool ("Jumping", false);
			
			jumpEndTime = Mathf.Infinity;
		}


		if (myState == PlayerState.IDLE) {

			DetectStartOfRun();

			DetectStartOfJump();

		}
		else if (myState == PlayerState.RUNNING) {

			DetectRunning();

			DetectStartOfRoll();

			DetectStartOfJump();

		}
		else if (myState == PlayerState.JUMPING) {
			
			if (canMoveWhileJumping){
				//DetectRunning();
			}
		}
		else if (myState == PlayerState.ROLLING){

		}

		DetectFall ();

		if (increaseGravityWhenFalling) {
			if (myRigidbody.velocity.y < 0)
				Physics.gravity = new Vector3 (0, -9.81f * gravityMultiplier*2f, 0);
			else
				Physics.gravity = new Vector3 (0, -9.81f * gravityMultiplier, 0);
		}

		if (faceWhereGoing)
			if (targetVelocity != Vector3.zero)
				transform.forward = targetVelocity;

	}

	void OnCollisionEnter(Collision collision){
		
		if (collision.transform.tag == "Floor") {
			if (myState == PlayerState.JUMPING)
			{
				myAnimator.SetBool("Jumping", false);

				myState = PlayerState.RUNNING;

				myAnimator.SetBool("Walking", true);
			}
		}
	}

	void DetectFall(){

		if (transform.position.y < respawnHeight)
			Application.LoadLevel (Application.loadedLevel);

	}

	void DetectStartOfRoll(){

		if (Input.GetKeyDown (KeyCode.R)) {
			Roll ();
		}

	}

	void DetectStartOfJump(){

		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}

	}

	void DetectStartOfRun(){

		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) {
			myAnimator.SetBool("Walking", true);
			myState = PlayerState.RUNNING;
		}
	}

	void DetectRunning(){

		bool runningInZDirection = false;

		if (Input.GetKey (KeyCode.W)) 
		{
			//GetComponent<Rigidbody>().AddForce(transform.forward*20);
			AccelerateFromToZ(forwardMinSpeed, forwardMaxSpeed, forwardAcceleration*Time.deltaTime);

			myAnimator.SetFloat("Speed", targetVelocity.z);

			runningInZDirection = true;
		}
		else if (Input.GetKey (KeyCode.S)) 
		{
			//GetComponent<Rigidbody>().AddForce(-transform.forward*20);
			AccelerateFromToZ(-forwardMinSpeed, -forwardMaxSpeed, -forwardAcceleration*Time.deltaTime);

			myAnimator.SetFloat("Speed", Mathf.Abs(targetVelocity.z));

			runningInZDirection = true;
		}
		else
		{

			targetVelocity.z = 0;

			if (stopSuddenly)
			{
				Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
				newVelocity.z = 0;
				GetComponent<Rigidbody>().velocity = newVelocity;
			}

		}

		if (Input.GetKey (KeyCode.A)) 
		{
			if (targetVelocity.x > 0) 
				targetVelocity.x = 0;

			AccelerateFromToX(-strafeMinSpeed, -strafeMaxSpeed, -strafeAcceleration*Time.deltaTime);
			
		}
		else if (Input.GetKey (KeyCode.D)) 
		{
			if (targetVelocity.x < 0) 
				targetVelocity.x = 0;

			AccelerateFromToX(strafeMinSpeed, strafeMaxSpeed, strafeAcceleration*Time.deltaTime);
		}
		else
		{
			targetVelocity.x = 0;

			if (stopSuddenly)
			{
				Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
				newVelocity.x = 0;
				GetComponent<Rigidbody>().velocity = newVelocity;
			}


			if (!runningInZDirection){
				myAnimator.SetBool("Walking", false);
				myState = PlayerState.IDLE;
			}
		}
	}

	void Jump(){

		Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
		newVelocity.y = jumpVelocity;
		GetComponent<Rigidbody>().velocity = newVelocity;

		myState = PlayerState.JUMPING;
		myAnimator.SetBool("Jumping", true);

		jumpEndTime = Time.time + jumpTimeOut;

	}

	void Roll(){
		
		Vector3 newVelocity = targetVelocity;
		newVelocity.Normalize ();
		newVelocity *= rollPower;
		GetComponent<Rigidbody>().velocity = newVelocity;
		
		myState = PlayerState.ROLLING;
		myAnimator.SetBool("Rolling", true);

		ScheduleEndOfRoll (rollDuration);
		
	}

	void ScheduleEndOfRoll(float duration){

		rollEndTime = Time.time + duration;

	}

	void AccelerateFromToX(float initialVelocity, float maxVelocity, float acceleration){

		Vector3 newVelocity = myRigidbody.velocity;

		//start at the initial velocity
		if (Mathf.Abs (targetVelocity.x) < Mathf.Abs (initialVelocity)) {
			targetVelocity.x = initialVelocity;
		}
		//accelerate up to the max
		else if (Mathf.Abs (targetVelocity.x) < Mathf.Abs (maxVelocity))
			targetVelocity.x += acceleration;
		//if youre greater or equal to the max, stay at the max
		else
			targetVelocity.x = maxVelocity;

		newVelocity.x = targetVelocity.x;

		myRigidbody.velocity = newVelocity;

	}

	void AccelerateFromToZ(float initialVelocity, float maxVelocity, float acceleration){
		
		Vector3 newVelocity = myRigidbody.velocity;
		
		//start at the initial velocity
		if (Mathf.Abs (targetVelocity.z) < Mathf.Abs (initialVelocity)) {
			targetVelocity.z = initialVelocity;
		}
		//accelerate up to the max
		else if (Mathf.Abs (targetVelocity.z) < Mathf.Abs (maxVelocity))
			targetVelocity.z += acceleration;
		//if youre greater or equal to the max, stay at the max
		else
			targetVelocity.z = maxVelocity;
		
		newVelocity.z = targetVelocity.z;
		
		myRigidbody.velocity = newVelocity;
		
	}
}
