using UnityEngine;
using System.Collections;


public class Trap : MonoBehaviour {

	public bool killPlayerOnTouch;

	public Animation myAnimation;


	// Use this for initialization
	void Start () {
		myAnimation = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){
		if (c.transform.tag == "Player"){
			PlayerStates s = c.transform.GetComponent<PlayerStates>();
			s.setState(PlayerStates.playerState.DEAD);
		}
	}
}
