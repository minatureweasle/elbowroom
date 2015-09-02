using UnityEngine;
using System.Collections;

public class SwitchScene : TriggeredTrap {

	public bool doorToHome;
	public string room;

	public GameObject player;
	int playerCount = 0; 

	void Start () {

	}
	

	void Update () {

	}

    public override void OnTrapActivated(Collider c){
		
		if (doorToHome) {
				//call time manager for score saving logic
			player.GetComponent<TimeManager>().saveTime(c.name);
		}
		playerCount++;
		if (playerCount >= 2){
			Application.LoadLevel(room);
		} 
	}

	public override void OnTrapDeactivated(Collider c){
		playerCount--;
		//Debug.Log (playerCount);
	}
	public override void OnTrapActive(){}

}
