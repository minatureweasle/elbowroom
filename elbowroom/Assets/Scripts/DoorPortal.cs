using UnityEngine;
using System.Collections;

public class DoorPortal : MonoBehaviour {
	
	public string room;

	int playerCount = 0;

    //end the race if needed, then change scenes if both players are at the door
    void OnTriggerEnter(Collider c){
        if (room == "HomeRoom") {
            RaceManager.instance.EndRace(c.name);
        }
        playerCount++;
        if (playerCount >= 2){
            SceneManager.instance.SwitchScenes(room);
        }
	}

    void OnTriggerExit(Collider c){
		playerCount--;
	}

    

	


}
