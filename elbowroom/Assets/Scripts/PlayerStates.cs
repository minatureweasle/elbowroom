using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour {

	public Transform startPoint;
	public enum playerState {ALIVE, DEAD};
	playerState state;

	void Start () {
		state = playerState.ALIVE;
	}
	
	void Update () {
	
		if (state == playerState.DEAD) {
			Respawn();
		}

	}

	void Respawn(){
		transform.position = startPoint.position;
	}

	public void setState(playerState s){
		state = s;
	}
}
