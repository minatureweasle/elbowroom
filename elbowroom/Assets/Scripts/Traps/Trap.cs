using UnityEngine;
using System.Collections;


public class Trap : MonoBehaviour {

	public bool killPlayerOnTouch;

	public bool pushPlayerOnTouch;

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
			if (pushPlayerOnTouch){
				EbbController s = c.transform.GetComponent<EbbController>();
				s.Recoil();
			}
			else if (killPlayerOnTouch){
				PlayerLogic pLogic = c.transform.GetComponent<PlayerLogic>();
				pLogic.setState(PlayerLogic.playerState.DEAD);
			}


		}
	}
}
