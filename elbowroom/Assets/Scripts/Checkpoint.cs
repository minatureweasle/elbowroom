using UnityEngine;
using System.Collections;

public class Checkpoint : TriggeredTrap {

	public Material activeMaterial;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTrapActivated(Collider c){
		if (c.tag == "Player") {
			PlayerStates s = c.transform.GetComponent<PlayerStates>();
			s.setStartPoint(transform.position + Vector3.up*6.25f);
			GetComponent<Renderer>().material = activeMaterial;
		}
	}
	
	public override void OnTrapDeactivated(Collider c){

	}
}
