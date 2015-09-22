using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
	//public bool isFirst = false;
	public Material activeMaterial;

	bool hasBeenActivated = false;

    //activate the checkpoint so that the player who touched it will return to it if they die
    //if this is the first checkpoint in the scene, start a race
	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player") {
			PlayerLogic s = c.transform.GetComponent<PlayerLogic> ();
			s.setStartPoint (transform.position + Vector3.up * 6.25f);
			GetComponent<Renderer> ().material = activeMaterial;
		}

		if (!hasBeenActivated) {
            RaceManager.instance.StartCountdown();
			hasBeenActivated = true;
		}
	}

}
