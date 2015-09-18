using UnityEngine;
using System.Collections;

public class PlayerGroup : MonoBehaviour {

	public static PlayerGroup instance;

	public Transform players;

	void Awake () {
		instance = this;
	}

	public bool IsAnyPlayerWithinRange(Vector3 target, float distance){
		for (int i = 0; i < players.childCount; i++) {
			if ((target - players.GetChild (i).transform.position).magnitude < distance)
				return true;
		}
		return false;
	}

    /*public void startTimers()
    {
        for (int i = 0; i < players.childCount; i++)
        {
            Transform currentPlayer = players.GetChild(i);
            currentPlayer.GetComponent<TimeManager>().startTimer();
        }
    }

    public void stopTimers()
    {
        for (int i = 0; i < players.childCount; i++)
        {
            Transform currentPlayer = players.GetChild(i);
            currentPlayer.GetComponent<TimeManager>().saveTime();
        }
    }*/
}
