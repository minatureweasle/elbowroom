using UnityEngine;
using System.Collections;

public class PlayerResetter : MonoBehaviour {

	Vector3 startPoint;

	public enum playerState {ALIVE, DEAD};
	playerState state;

	void Start () {
		state = playerState.ALIVE;
	}

	//Change the player's state and react to the player's state
	void Update () {
        //return the player to the last checkpoint if they have fallen
        if (transform.position.y < -20)
            state = playerState.DEAD;

		if (state == playerState.DEAD) {
			Respawn();
		}
	}

    //Return the player to its starting point and set its state to ALIVE
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
