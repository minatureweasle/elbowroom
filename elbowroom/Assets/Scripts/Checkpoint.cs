using UnityEngine;
using System.Collections;

public class Checkpoint : TriggeredTrap
{

	public GameObject _SceneManager;
	public bool isFirst = false;
	public Material activeMaterial;

	bool hasBeenActivated = false;

	public override void OnTrapActivated (Collider c)
	{
		if (c.tag == "Player") {
			PlayerLogic s = c.transform.GetComponent<PlayerLogic> ();
			s.setStartPoint (transform.position + Vector3.up * 6.25f);
			GetComponent<Renderer> ().material = activeMaterial;
		}

		if (isFirst && !hasBeenActivated) {
			StartGame sG = _SceneManager.GetComponent<StartGame> ();
			sG.StartCountdown ();
			hasBeenActivated = true;
		}
	}
	
	public override void OnTrapDeactivated (Collider c){}

	public override void OnTrapActive (){}
}
