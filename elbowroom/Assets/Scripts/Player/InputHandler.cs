using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	KeyCode forwardKey;
	KeyCode backwardKey;
	KeyCode leftKey;
	KeyCode rightKey;
	KeyCode jumpKey;
	KeyCode boostKey;

	public enum PlayerIdentity {PLAYER1, PLAYER2};
	public PlayerIdentity myIdentity;

	//set the input keys depending on the player's identity
	void Start () {
		if (myIdentity == PlayerIdentity.PLAYER1) {
			forwardKey = KeyCode.W;
			backwardKey = KeyCode.S;
			leftKey = KeyCode.A;
			rightKey = KeyCode.D;
			jumpKey = KeyCode.Space;
			boostKey = KeyCode.R;
		} else {
			forwardKey = KeyCode.UpArrow;
			backwardKey = KeyCode.DownArrow;
			leftKey = KeyCode.LeftArrow;
			rightKey = KeyCode.RightArrow;
			jumpKey = KeyCode.RightControl;
			boostKey = KeyCode.Slash;
		}
	}

    //returns true if the player pressed their forward, backward, left or right keys this frame
	public bool PressedAnyDirectionalKey(){
		if (Input.GetKeyDown (forwardKey) || Input.GetKeyDown (backwardKey) || Input.GetKeyDown (leftKey) || Input.GetKeyDown (rightKey)) {
			return true;
		}
		return false;
	}

    //returns true if the player is holding down their forward key on this frame
	public bool IsPressingForward(){
		if (Input.GetKey (forwardKey))
			return true;
		return false;
	}

    //returns true if the player is holding down their backward key on this frame
	public bool IsPressingBackward(){
		if (Input.GetKey (backwardKey))
			return true;
		return false;
	}

    //returns true if the player is holding down their left key on this frame
	public bool IsPressingLeft(){
		if (Input.GetKey (leftKey))
			return true;
		return false;
	}

    //returns true if the player is holding down their right key on this frame
	public bool IsPressingRight(){
		if (Input.GetKey (rightKey))
			return true;
		return false;
	}

    //returns true if the player is holding down their boost key on this frame
    public bool IsPressingBoost()
    {
        if (Input.GetKey(boostKey))
            return true;
        return false;
    }

    //returns true if the player pressed the jump key on this frame
	public bool PressedJump(){
		if (Input.GetKeyDown (jumpKey))
			return true;
		return false;
	}

    //returns true if the player pressed their boost key on this frame
	public bool PressedBoost(){
		if (Input.GetKeyDown (boostKey))
			return true;
		return false;
	}

    
}
