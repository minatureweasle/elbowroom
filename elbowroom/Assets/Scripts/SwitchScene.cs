using UnityEngine;
using System.Collections;

public class SwitchScene : TriggeredTrap {
	
	public string room;
	public bool doorToHome;

	public GameObject player;

	int playerCount = 0;

    public override void OnTrapActivated(Collider c){
		if (doorToHome)
			player.GetComponent<TimeManager>().saveTime(c.name);
		playerCount++;
		if (playerCount >= 2)
			Application.LoadLevel(room);
	}

	public override void OnTrapActive(){}

	public override void OnTrapDeactivated(Collider c){
		playerCount--;
	}


}
