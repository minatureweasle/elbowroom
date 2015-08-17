using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour {

	public Vector3 startPoint;
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
		transform.position = startPoint;
	}

	public void setState(playerState s){
		state = s;
	}

	public void setStartPoint(Vector3 t){
		startPoint = t;
	}
}
