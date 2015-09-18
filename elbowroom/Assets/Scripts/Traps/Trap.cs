using UnityEngine;
using System.Collections;


public class Trap : MonoBehaviour {

	public enum TrapAction {NONE, PUSH, KILL};
	public TrapAction trapAction = TrapAction.NONE;

    //When a player collides with this trap, it does an action depending on what the trap action is set to
	void OnCollisionEnter(Collision c){
		if (c.transform.tag == "Player"){
			if (trapAction == TrapAction.PUSH){
				PlayerController s = c.transform.GetComponent<PlayerController>();
				s.Recoil();
			}
			else if (trapAction == TrapAction.KILL){
				PlayerLogic pLogic = c.transform.GetComponent<PlayerLogic>();
				pLogic.setState(PlayerLogic.playerState.DEAD);
			}
		}
	}
}
