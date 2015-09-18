using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Animator myAnimator;
	Rigidbody myRigidbody;
	InputHandler myInputDetection;

	Vector3 targetVelocity;

	public bool useForce = false;

	public bool increaseGravityWhenFalling = true;

	public bool canMoveWhileJumping = true;

	public bool stopSuddenly = true;

	public float jumpVelocity = 16f;

	public float boostDuration = 0.7f;
	public float boostMaxSpeed = 36;
	public float boostAcceleration = 100;

	public float jumpTimeOut = 0.7f;
	float recoilTimeOut = 0.7f;

	public float wallJumpTimeWindow = 0.7f;
	float wallJumpAvailabilityEnd = 0;

	public float respawnHeight = -20;

	public float strafeMinSpeed = 0;
	public float strafeMaxSpeed = 16;
	public float strafeAcceleration = 30;

	public float forwardMinSpeed = 3;
	public float forwardMaxSpeed = 18;
	public float forwardAcceleration = 10;

	float currentMaxSpeed;
	float currentAcceleration;

	float boostEndTime = Mathf.Infinity;
	float jumpEndTime = Mathf.Infinity;
	float recoilEndTime = Mathf.Infinity;

	public enum PlayerState {IDLE, RUNNING, JUMPING, BOOSTING, RECOILING};

	PlayerState myState = PlayerState.IDLE;

	void Start () {

		myAnimator = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody> ();
		myInputDetection = GetComponent<InputHandler> ();

		currentMaxSpeed = forwardMaxSpeed;
		currentAcceleration = forwardAcceleration;

	}

	void Update () {
		//Debug.Log (myState);

		if (Time.time > boostEndTime) {

			currentAcceleration = forwardAcceleration;
			currentMaxSpeed = forwardMaxSpeed;

			//might have changed to jumping. if you didn't, and finished a boost without changing state, change back to jumping
			if (myState == PlayerState.BOOSTING){
				myState = PlayerState.RUNNING;

				myAnimator.SetBool ("Rolling", false);
			}

			boostEndTime = Mathf.Infinity;
		}

		if (Time.time > jumpEndTime) {

			myState = PlayerState.RUNNING;

			myAnimator.SetBool ("Jumping", false);
			
			jumpEndTime = Mathf.Infinity;
		}

		if (Time.time > recoilEndTime) {
			
			myState = PlayerState.RUNNING;
			
			myAnimator.SetBool ("Running", true);
			
			recoilEndTime = Mathf.Infinity;
		}

		if (Time.time < wallJumpAvailabilityEnd) {
			
			DetectWallJump();
		}


		if (myState == PlayerState.IDLE) {

			DetectRun();

			DetectJump();

			DetectBoost();

		}
		else if (myState == PlayerState.RUNNING) {

			Run();

			DetectBoost();

			DetectJump();

			DetectNotRunning ();

		}
		else if (myState == PlayerState.JUMPING) {
			
			if (canMoveWhileJumping){

				Run();
			}
		}
		else if (myState == PlayerState.BOOSTING){

			Run();

			DetectJump();
			
			DetectNotRunning ();

		}
		else if (myState == PlayerState.RECOILING){			
			
		}

		//return the player to the last checkpoint if they have fallen
		if (transform.position.y < respawnHeight)
			GetComponent<PlayerLogic> ().setState (PlayerLogic.playerState.DEAD);

		//fall faster than you rise
		if (increaseGravityWhenFalling) {
			if (myRigidbody.velocity.y < 0)
				myRigidbody.velocity += Physics.gravity*Time.deltaTime;
		}

		//face the direction in which you are going
		if (targetVelocity != Vector3.zero) transform.forward = targetVelocity;

	}

	void OnCollisionEnter(Collision collision){
		
		if (collision.transform.tag == "Floor") {
			if (myState == PlayerState.JUMPING)
			{
				myAnimator.SetBool("Jumping", false);
				
				myState = PlayerState.RUNNING;
				
				myAnimator.SetBool("Running", true);
			}
		}
	}

	public void Recoil(){

		myAnimator.SetBool("Jumping", false);
		myAnimator.SetBool("Running", false);

		myState = PlayerState.RECOILING;
		
		AccelerateFromToZ (-20, -20, 0);

		recoilEndTime = Time.time + recoilTimeOut;

	}

	//========================================
	//RUNNING
	//========================================

	void DetectRun(){

		if (myInputDetection.PressedAnyDirectionalKey()) {
			myAnimator.SetBool("Running", true);
			myState = PlayerState.RUNNING;
		}
	}

	void Run(){

		//bool runningInZDirection = false;

		if (myInputDetection.IsPressingForward()) 
		{
			if (useForce){
				if (myRigidbody.velocity.z < currentMaxSpeed){
					myRigidbody.AddForce(Vector3.forward*currentAcceleration*20f);
				}

				targetVelocity.z = myRigidbody.velocity.z;
			}
			else{
				AccelerateFromToZ(forwardMinSpeed, currentMaxSpeed, currentAcceleration*Time.deltaTime);
			}

			myAnimator.SetFloat("Speed", targetVelocity.z);

		}
		else if (myInputDetection.IsPressingBackward()) 
		{
			//myRigidbody.AddForce(-transform.forward*20);
			AccelerateFromToZ(-forwardMinSpeed, -currentMaxSpeed, -currentAcceleration*Time.deltaTime);

			myAnimator.SetFloat("Speed", Mathf.Abs(targetVelocity.z));

		}
		else
		{
			targetVelocity.z = 0;

			if (stopSuddenly)
			{
				Vector3 newVelocity = myRigidbody.velocity;
				newVelocity.z = 0;
				myRigidbody.velocity = newVelocity;
			}

		}

		if (myInputDetection.IsPressingLeft()) 
		{
			if (targetVelocity.x > 0) 
				targetVelocity.x = 0;

			AccelerateFromToX(-strafeMinSpeed, -strafeMaxSpeed, -strafeAcceleration*Time.deltaTime);
			
		}
		else if (myInputDetection.IsPressingRight()) 
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
				Vector3 newVelocity = myRigidbody.velocity;
				newVelocity.x = 0;
				myRigidbody.velocity = newVelocity;
			}
		}

		//DetectNotRunning ();
	}

	void DetectNotRunning(){
		
		if (targetVelocity.x == 0 && targetVelocity.z == 0) {
			myAnimator.SetBool ("Running", false);
			myState = PlayerState.IDLE;
		}
		
	}

	//========================================
	//JUMPING
	//========================================

	void DetectJump(){
		
		if (myInputDetection.PressedJump()) {
			Jump ();
		}
		
	}

	void Jump(){

		Vector3 newVelocity = myRigidbody.velocity;
		newVelocity.y = jumpVelocity;
		myRigidbody.velocity = newVelocity;

		myState = PlayerState.JUMPING;
		myAnimator.SetBool("Jumping", true);

		jumpEndTime = Time.time + jumpTimeOut;

	}

	//========================================
	//WALL JUMPING
	//========================================

	public void AllowWallJump(){

		wallJumpAvailabilityEnd = Time.time + wallJumpTimeWindow;
	}

	void DetectWallJump(){
		if (myInputDetection.PressedJump()) {

			WallJump();
		}

	}

	void WallJump(){
		Vector3 newVelocity = myRigidbody.velocity;
		newVelocity.y = jumpVelocity/3f;
		
		newVelocity.x = -1f*((transform.position.x)/Mathf.Abs(transform.position.x))*13f;
		
		newVelocity.z = 15f;

		myRigidbody.velocity = newVelocity;
		
		myState = PlayerState.JUMPING;
		myAnimator.SetBool("Jumping", true);
		
		jumpEndTime = Time.time + 2f;
		
		//wall jumping is no longer available
		wallJumpAvailabilityEnd = 0;
	}

	//========================================
	//BOOSTING
	//========================================

	void DetectBoost(){


		if (myInputDetection.PressedBoost()) {

			//if the previous roll has ended
			if (myState != PlayerState.BOOSTING){
				Boost ();
			}
		}

		
	}

	void Boost(){

		currentAcceleration = boostAcceleration;
		currentMaxSpeed = boostMaxSpeed;

		myState = PlayerState.BOOSTING;
		//myAnimator.SetBool("Rolling", true);
		
		boostEndTime = Time.time + boostDuration;
		
	}

	//========================================
	//CONTROLLING PLAYER VELOCITY
	//========================================

	void AccelerateFromToX(float initialVelocity, float maxVelocity, float acceleration){

		Vector3 newVelocity = myRigidbody.velocity;

		//start at the initial velocity
		if (Mathf.Abs (targetVelocity.x) < Mathf.Abs (initialVelocity)) {
			targetVelocity.x = initialVelocity;
		}
		//accelerate up to the max
		else if (Mathf.Abs (targetVelocity.x) < Mathf.Abs (maxVelocity))
			targetVelocity.x += acceleration*Time.deltaTime*55f;
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
			targetVelocity.z += acceleration*Time.deltaTime*55f;
		//if youre greater or equal to the max, stay at the max
		else
			targetVelocity.z = Mathf.Lerp(targetVelocity.z, maxVelocity, 0.1f);//targetVelocity.z = maxVelocity;
		
		newVelocity.z = targetVelocity.z;
		
		myRigidbody.velocity = newVelocity;
		
	}
}
