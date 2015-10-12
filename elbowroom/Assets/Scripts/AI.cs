using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	public float minAheadDistance = 10f;
	public float acceleration = 0.3f;
	public float sidewaysDistance = 2f;
	public GameObject myTurret; 
	public GameObject myRotor;
	public enum states {TARGETING, FLOATING}; 
	private states state;

	private float moveAroundPoint;

	void Start () {
		state = states.FLOATING; 
		moveAroundPoint = transform.position.x; 
	}
	
	void Update () {
		
		moveSideways();
		changeState(); 
		renderState(); 
		tiltWhereGoing();
	}

	void tiltWhereGoing(){
		transform.up = Vector3.Lerp(transform.up, Vector3.up + GetComponent<Rigidbody>().velocity*0.02f, 0.07f);
	}

	void renderState(){
		if (state == states.TARGETING){
			RocketLauncher rL = myTurret.GetComponent<RocketLauncher>();
			rL.TargetPlayer();
			GetComponent<Rigidbody>().velocity += new Vector3(0,0,acceleration);
			Vector3 closestPlayerPosition = PlayerGroup.instance.GetClosestPlayerTo(transform.position).position;
		}
		else if (state == states.FLOATING){
			Vector3 oldVelocity = GetComponent<Rigidbody>().velocity; //GetComponent<Rigidbody>().velocity*0.5f;//Vector3.zero;
			Vector3 newVelocity = new Vector3(oldVelocity.x, oldVelocity.y, oldVelocity.z*0.5f);
			GetComponent<Rigidbody>().velocity = newVelocity;
		}
	}

	void changeState(){
		// should we change to the targeting state
		Vector3 closestPlayerPosition = PlayerGroup.instance.GetClosestPlayerTo(transform.position).position;
		if (Mathf.Abs(closestPlayerPosition.z - transform.position.z) < minAheadDistance){
			state = states.TARGETING;
		}
		else{
			state = states.FLOATING;
		}
	}

	void moveSideways(){
		bool moveLeft = false;
		if (transform.position.x >= moveAroundPoint + sidewaysDistance){
			moveLeft = true; 
		}
		else if (transform.position.x <= moveAroundPoint - sidewaysDistance){
			moveLeft = false; 
		}
		if (moveLeft == true){
			GetComponent<Rigidbody>().velocity -= new Vector3(0.5f,0,0); 
		}
		else{
			GetComponent<Rigidbody>().velocity += new Vector3(0.5f,0,0);
		}
	}

}
