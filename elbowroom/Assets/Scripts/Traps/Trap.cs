using UnityEngine;
using System.Collections;


public class Trap : MonoBehaviour {

	public enum TrapAction {NONE, PUSH, KILL};
	public TrapAction trapAction = TrapAction.NONE;

	void OnCollisionEnter(Collision c){
		if (c.transform.tag == "Player"){
			if (trapAction == TrapAction.PUSH){
				EbbController s = c.transform.GetComponent<EbbController>();
				s.Recoil();
			}
			else if (trapAction == TrapAction.KILL){
				PlayerLogic pLogic = c.transform.GetComponent<PlayerLogic>();
				pLogic.setState(PlayerLogic.playerState.DEAD);
			}
		}
	}
}
