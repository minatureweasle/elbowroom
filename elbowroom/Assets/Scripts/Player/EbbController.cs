using UnityEngine;
using System.Collections;

public class EbbController : MonoBehaviour {

	Animator myAnimator;

	Rigidbody myRigidbody;

	InputDetection myInputDetection;

	Vector3 targetVelocity;

	public bool increaseGravityWhenFalling = true;

	public bool canMoveWhileJumping = true;

	public bool stopSuddenly = true;

	//public float gravityMultiplier = 4f;

	public float jumpVelocity = 15f;

	public float rollPower = 30;

	public float rollDuration = 0.5f;

	public float jumpTimeOut = 0.7f;
	float recoilTimeOut = 0.7f;

	public float wallJumpTimeWindow = 0.7f;
	float wallJumpAvailabilityEnd = 0;

	public float respawnHeight = -25;

	public float strafeMinSpeed = 0;
	public float strafeMaxSpeed = 16;
	public float strafeAcceleration = 30;

	public float forwardMinSpeed = 3;
	public float forwardMaxSpeed = 18;
	public float forwardAcceleration = 10;

	float rollEndTime = Mathf.Infinity;
	float jumpEndTime = Mathf.Infinity;
	float recoilEndTime = Mathf.Infinity;

	public enum PlayerState {IDLE, RUNNING, JUMPING, ROLLING, RECOILING};

	PlayerState myState = PlayerState.IDLE;

	void Start () {

		myAnimator = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody> ();
		myInputDetection = GetComponent<InputDetection> ();


		//Physics.gravity = new Vector3 (0, -9.81f * gravityMultiplier, 0);
	}

	void Update () {
		//Debug.Log (myState);

		if (Time.time > rollEndTime) {

			forwardAcceleration *= 0.1f;
			forwardMaxSpeed *= 0.5f;

			myState = PlayerState.RUNNING;

			myAnimator.SetBool ("Rolling", false);

			rollEndTime = Mathf.Infinity;
		}

		if (Time.time > jumpEndTime) {

			myState = PlayerState.RUNNING;

			myAnimator.SetBool ("Jumping", false);
			
			jumpEndTime = Mathf.Infinity;
		}

		if (Time.time > recoilEndTime) {
			
			myState = PlayerState.RUNNING;
			
			myAnimator.SetBool ("Walking", true);
			
			recoilEndTime = Mathf.Infinity;
		}

		if (Time.time < wallJumpAvailabilityEnd) {
			
			DetectWallJump();
		}


		if (myState == PlayerState.IDLE) {

			DetectRun();

			DetectJump();

		}
		else if (myState == PlayerState.RUNNING) {

			Run();

			DetectRoll();

			DetectJump();

			DetectNotRunning ();

		}
		else if (myState == PlayerState.JUMPING) {
			
			if (canMoveWhileJumping){

				Run();
			}
		}
		else if (myState == PlayerState.ROLLING){

			Run();
			
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

	public void Recoil(){

		myAnimator.SetBool("Jumping", false);
		myAnimator.SetBool("Walking", false);

		myState = PlayerState.RECOILING;
		
		AccelerateFromToZ (-20, -20, 0);

		recoilEndTime = Time.time + recoilTimeOut;

	}

	//========================================
	//RUNNING
	//========================================

	void DetectRun(){

		if (myInputDetection.PressedAnyDirectionalKey()) {
			myAnimator.SetBool("Walking", true);
			myState = PlayerState.RUNNING;
		}
	}

	void Run(){

		//bool runningInZDirection = false;

		if (myInputDetection.IsPressingForward()) 
		{
			//myRigidbody.AddForce(transform.forward*20);
			AccelerateFromToZ(forwardMinSpeed, forwardMaxSpeed, forwardAcceleration*Time.deltaTime);

			myAnimator.SetFloat("Speed", targetVelocity.z);

			//runningInZDirection = true;
		}
		else if (myInputDetection.IsPressingBackward()) 
		{
			//myRigidbody.AddForce(-transform.forward*20);
			AccelerateFromToZ(-forwardMinSpeed, -forwardMaxSpeed, -forwardAcceleration*Time.deltaTime);

			myAnimator.SetFloat("Speed", Mathf.Abs(targetVelocity.z));

			//runningInZDirection = true;
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
			myAnimator.SetBool ("Walking", false);
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
	//ROLLING
	//========================================

	void DetectRoll(){
		
		if (myInputDetection.PressedRoll()) {

			//if the previous roll has ended
			if (myState != PlayerState.ROLLING){
				Roll ();
			}
		}
		
	}

	void Roll(){
		
		/*Vector3 newVelocity = targetVelocity;
		newVelocity.Normalize ();
		newVelocity *= rollPower;
		myRigidbody.velocity = newVelocity;

		myState = PlayerState.ROLLING;
		myAnimator.SetBool("Rolling", true);

		rollEndTime = Time.time + rollDuration;*/

		forwardAcceleration *= 10;
		forwardMaxSpeed *= 2;

		myState = PlayerState.ROLLING;
		//myAnimator.SetBool("Rolling", true);
		
		rollEndTime = Time.time + rollDuration;
		
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
			targetVelocity.z = maxVelocity;
		
		newVelocity.z = targetVelocity.z;
		
		myRigidbody.velocity = newVelocity;
		
	}
}
