using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour {

	public Transform respawnPosition;
	public enum playerState {ALIVE, DEAD};
	playerState state;

	void Start () {
		state = playerState.ALIVE;
	}
	
	void Update () {
	
		if (state = playerState.DEAD) {
			Respawn();
		}

	}

	void Respawn(){
		transform.position = respawnPosition;
	}

	public void setState(playerState s){
		state = s;
	}
}
