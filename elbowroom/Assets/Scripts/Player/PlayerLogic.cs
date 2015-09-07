using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour {

	Vector3 startPoint;

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
		state = playerState.ALIVE;
	}

	public void setState(playerState s){
		state = s;
	}

	public void setStartPoint(Vector3 t){
		startPoint = t;
	}
}
