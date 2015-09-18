using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{

	public RaceManager raceManager;
	public bool isFirst = false;
	public Material activeMaterial;

	bool hasBeenActivated = false;

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player") {
			PlayerLogic s = c.transform.GetComponent<PlayerLogic> ();
			s.setStartPoint (transform.position + Vector3.up * 6.25f);
			GetComponent<Renderer> ().material = activeMaterial;
		}

		if (isFirst && !hasBeenActivated) {
            raceManager.StartCountdown();
			hasBeenActivated = true;
		}
	}

}
