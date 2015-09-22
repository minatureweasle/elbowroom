using UnityEngine;
using System.Collections;


public class Trap : MonoBehaviour {

	public enum TrapAction {NONE, PUSH, KILL};
	public TrapAction trapAction = TrapAction.NONE;

    //When a player collides with this trap, it does an action depending on what the trap action is set to
	void OnCollisionEnter(Collision c){
		if (c.transform.tag == "Player"){
			if (trapAction == TrapAction.PUSH){
				PlayerLogic s = c.transform.GetComponent<PlayerLogic>();
				s.Recoil();
			}
			else if (trapAction == TrapAction.KILL){
				PlayerResetter pLogic = c.transform.GetComponent<PlayerResetter>();
				pLogic.setState(PlayerResetter.playerState.DEAD);
			}
		}
	}
}
