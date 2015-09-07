using UnityEngine;
using System.Collections;

public class SwitchScene : TriggeredTrap {
	
	public string room;
	public bool doorToHome;

	public GameObject player;

	public Fade fader;

	int playerCount = 0;

    public override void OnTrapActivated(Collider c){
		if (doorToHome)
			player.GetComponent<TimeManager>().saveTime(c.name);
		playerCount++;
		if (playerCount >= 2)
			StartCoroutine(FadeAndExit());
	}

	public override void OnTrapActive(){}

	public override void OnTrapDeactivated(Collider c){
		playerCount--;
	}

	IEnumerator FadeAndExit(){
		fader.GetComponent<Fade>().fadeOut();
		yield return new WaitForSeconds(0.6f);
		Application.LoadLevel(room);
	}


}
