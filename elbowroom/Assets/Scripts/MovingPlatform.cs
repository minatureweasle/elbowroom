using UnityEngine;
using System.Collections;

public class MovingPlatform : TriggeredTrap {

	public bool moveHorizontal;
	public bool moveVertical;
	public bool flipLeft;
	public bool flipRight;
	public bool shrink;
	public float shrinkAmount;

	public int timeBetweenFlips = 2;
	private float flipInterval;
	public float timeUntilNextFlip;

	public int horizontalMoveDistance = 4;
	public int verticalMoveDistance = 4;

	public float moveSpeed = 5.0f;

	private bool moveLeft = true;

	Animator platformAnimator;

	Vector3 startingPosition;

	public GameObject platformMat;

    //store initial data and cache components
	void Start () {
		startingPosition = transform.position;
		flipInterval = Time.time + timeBetweenFlips;
		platformAnimator = GetComponent<Animator> ();

	}

    //check what actions the platform should take while it is active
	void Update(){

		timeUntilNextFlip = flipInterval - Time.time;
	
		//platformMat.renderer.material.color = new Color ( 0.4f + (timeBetweenFlips - timeUntilNextFlip) , 1f ,  0.6f);
		if (flipLeft || flipRight) {
			platformMat.renderer.material.color = new Color (1 , timeUntilNextFlip ,  timeUntilNextFlip);
		}

		bool canFlip = false;
		if (timeUntilNextFlip <= 0) {
			canFlip = true;
			flipInterval = Time.time + timeBetweenFlips;
		}
		if (shrink) {
			pShrink();
		}

		if (moveHorizontal){
			moveLeft = pMoveHorizontal(moveLeft);
		}
		if (moveVertical){
			pMoveVertical();
		}
		if (canFlip & flipLeft){
			pFlipLeft();
			canFlip = false;
		}
		else if (canFlip & flipRight){
			pFlipRight();
			canFlip = false;
		}
	}

    //smoothly move the platform to a specified distance away, left or right
    bool pMoveHorizontal(bool moveLeft){

		if (moveLeft) {
			Vector3 destination = startingPosition;
			destination.x = startingPosition.x + horizontalMoveDistance;
			transform.localPosition = Vector3.Slerp (transform.localPosition, destination, moveSpeed * Time.deltaTime);
			if (destination == transform.localPosition){
				return false;
			}
			return true;

		} else {
			Vector3 destination = startingPosition;
			destination.x = startingPosition.x -horizontalMoveDistance;
			transform.localPosition = Vector3.Slerp (transform.localPosition, destination, moveSpeed * Time.deltaTime);
			if (destination == transform.localPosition){
				return true;
			}
			return false;
		}
	}

	void pMoveVertical(){
		//amazing
	}

    //shrink the scale of the platform
	void pShrink(){

		if (transform.localScale.x <= 0) {
			GameObject.Destroy(this);
		}

		transform.localScale -= new Vector3(shrinkAmount,0,0);
		renderer.material.color += new Color(0,0,shrinkAmount);
	}

    //play an animation in which the platform rotates anticlockwise
	void pFlipLeft(){
		platformAnimator.Play ("FlipLeft");
	}

    //play an animation in which the platform rotates clockwise
	void pFlipRight(){
		platformAnimator.Play ("FlipRight");
	}
}
