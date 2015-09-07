using UnityEngine;
using System.Collections;


public class Trap : MonoBehaviour {

	public enum OnTouchAction {NONE, PUSH, KILL};
	public OnTouchAction myTrapAction = OnTouchAction.NONE;

	void OnCollisionEnter(Collision c){
		if (c.transform.tag == "Player"){
			if (myTrapAction == OnTouchAction.PUSH){
				EbbController s = c.transform.GetComponent<EbbController>();
				s.Recoil();
			}
			else if (myTrapAction == OnTouchAction.KILL){
				PlayerLogic pLogic = c.transform.GetComponent<PlayerLogic>();
				pLogic.setState(PlayerLogic.playerState.DEAD);
			}
		}
	}
}
