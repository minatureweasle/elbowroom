using UnityEngine;
using System.Collections;

public class Checkpoint : TriggeredTrap {

	public Material activeMaterial;
	public bool isFirst = false;
	public GameObject _SceneManager;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTrapActivated(Collider c){
		if (c.tag == "Player") {
			PlayerLogic s = c.transform.GetComponent<PlayerLogic>();
			s.setStartPoint(transform.position + Vector3.up*6.25f);
			GetComponent<Renderer>().material = activeMaterial;
		}
		if (isFirst) {
			StartGame sG = _SceneManager.GetComponent<StartGame>();
			sG.NewRace();
		}
	}
	
	public override void OnTrapDeactivated(Collider c){

	}

	public override void OnTrapActive(){
	}
}
