using UnityEngine;
using System.Collections;

public class SwitchScene : TriggeredTrap {

	public bool doorToHome;
	public string room;

	void Start () {
	
	}
	

	void Update () {

	}

    public override void OnTrapActivated(Collider c){
		
		if (doorToHome) {
				//call time manager for score saving logic
		}
		Application.LoadLevel(room);
	}

	public override void OnTrapDeactivated(Collider c){}
	public override void OnTrapActive(){}

}
