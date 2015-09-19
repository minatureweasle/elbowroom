using UnityEngine;
using System.Collections;

public class PlayerGroup : MonoBehaviour {

	public static PlayerGroup instance;

	public Transform players;

    //store a static instance for easy access of this class's public functions from other classes
	void Awake () {
		instance = this;
	}

    //return true if any of the players in the 'players' Transform is closer than the specified distance
	public bool IsAnyPlayerWithinRange(Vector3 target, float distance){
		for (int i = 0; i < players.childCount; i++) {
			if ((target - players.GetChild (i).transform.position).magnitude < distance)
				return true;
		}
		return false;
	}
}
